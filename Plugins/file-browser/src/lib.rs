use rfd::FileDialog;
use std::ffi::{CStr, CString};
use std::os::raw::c_char;
use std::path::PathBuf;

static mut STRING_POINTER: *mut c_char = 0 as *mut c_char;

#[no_mangle]
pub extern "C" fn free_memory() {
    unsafe {
        if STRING_POINTER.is_null() {
            return;
        }
        CString::from_raw(STRING_POINTER)
    };
}

#[no_mangle]
pub extern "C" fn open_file_dialog(
    title: *const c_char,
    dir: *const c_char,
    exts: *const c_char,
    multiple: bool,
) -> *const c_char {
    let title = c_char_to_string(title);
    let dir = c_char_to_string(dir);
    let extensions = parse_file_types(exts);

    let files = get_file_paths(title, dir, extensions, multiple);

    let mut paths = String::new();
    for file in files {
        paths.push_str(file.to_str().unwrap());
        paths.push_str(";");
    }

    let c_str = match CString::new(paths) {
        Ok(c_str) => c_str,
        Err(_) => return 0 as *const c_char,
    };
    let pntr = c_str.into_raw();
    unsafe {
        STRING_POINTER = pntr;
    }
    return pntr;
}

fn get_file_paths(
    title: &str,
    dir: &str,
    extensions: Vec<&'static str>,
    multiple: bool,
) -> Vec<PathBuf> {
    let mut dialog = FileDialog::new().add_filter("Files", &extensions);

    if title.len() > 0 {
        dialog = dialog.set_title(&title);
    }
    if dir.len() > 0 {
        dialog = dialog.set_directory(&dir);
    }
    match multiple {
        true => match dialog.pick_files() {
            Some(files) => files,
            None => return Vec::new(),
        },
        false => match dialog.pick_file() {
            Some(file) => vec![file],
            None => return Vec::new(),
        },
    }
}

fn c_char_to_string(c_str: *const c_char) -> &'static str {
    let c_str = unsafe { CStr::from_ptr(c_str) };
    let string = match c_str.to_str() {
        Ok(string) => string,
        Err(_) => return "",
    };
    return string;
}

fn parse_file_types(types: *const c_char) -> Vec<&'static str> {
    let file_types = c_char_to_string(types);
    let extensions = file_types.split(";").collect();
    return extensions;
}
