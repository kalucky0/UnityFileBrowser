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
    let ptr = c_str.into_raw();
    unsafe {
        STRING_POINTER = ptr;
    }
    return ptr;
}

#[no_mangle]
pub extern "C" fn open_folder_dialog(
    title: *const c_char,
    dir: *const c_char,
    multiple: bool,
) -> *const c_char {
    let title = c_char_to_string(title);
    let dir = c_char_to_string(dir);

    let files = get_folder_paths(title, dir, multiple);

    let mut paths = String::new();
    for file in files {
        paths.push_str(file.to_str().unwrap());
        paths.push_str(";");
    }

    let c_str = match CString::new(paths) {
        Ok(c_str) => c_str,
        Err(_) => return 0 as *const c_char,
    };
    let ptr = c_str.into_raw();
    unsafe {
        STRING_POINTER = ptr;
    }
    return ptr;
}

#[no_mangle]
pub extern fn open_save_dialog(
    title: *const c_char,
    dir: *const c_char,
    name: *const c_char,
    exts: *const c_char,
) -> *const c_char {
    let title = c_char_to_string(title);
    let dir = c_char_to_string(dir);
    let name = c_char_to_string(name);
    let extensions = parse_file_types(exts);

    let path = get_save_path(title, dir, name, extensions);

    let path = match path.to_str() {
        Some(path) => path,
        None => return 0 as *const c_char,
    };

    let c_str = match CString::new(path) {
        Ok(c_str) => c_str,
        Err(_) => return 0 as *const c_char,
    };
    let ptr = c_str.into_raw();
    unsafe {
        STRING_POINTER = ptr;
    }
    return ptr;
}

fn get_file_paths(
    title: &str,
    dir: &str,
    extensions: Vec<&'static str>,
    multiple: bool,
) -> Vec<PathBuf> {
    let mut dialog = FileDialog::new();

    if extensions.len() > 0 {
        dialog = dialog.add_filter("Files", &extensions);
    }
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

fn get_folder_paths(
    title: &str,
    dir: &str,
    multiple: bool,
) -> Vec<PathBuf> {
    let mut dialog = FileDialog::new();

    if title.len() > 0 {
        dialog = dialog.set_title(&title);
    }
    if dir.len() > 0 {
        dialog = dialog.set_directory(&dir);
    }
    match multiple {
        true => match dialog.pick_folders() {
            Some(files) => files,
            None => return Vec::new(),
        },
        false => match dialog.pick_folder() {
            Some(file) => vec![file],
            None => return Vec::new(),
        },
    }
}

fn get_save_path(
    title: &str,
    dir: &str,
    name: &str,
    extensions: Vec<&'static str>,
) -> PathBuf {
    let mut dialog = FileDialog::new();

    if extensions.len() > 0 {
        dialog = dialog.add_filter("Files", &extensions);
    }
    if title.len() > 0 {
        dialog = dialog.set_title(&title);
    }
    if dir.len() > 0 {
        dialog = dialog.set_directory(&dir);
    }
    if name.len() > 0 {
        dialog = dialog.set_file_name(&name);
    }
    match dialog.save_file() {
        Some(file) => file,
        None => return PathBuf::new(),
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
