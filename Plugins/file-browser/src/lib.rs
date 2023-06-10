use rfd::FileDialog;
use std::ffi::{CStr, CString};
use std::os::raw::c_char;

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
pub extern "C" fn open_file_dialog(file_types: *const c_char) -> *const c_char {
    let extensions = parse_file_types(file_types);
    let files = FileDialog::new()
        .add_filter("Files", &extensions)
        .pick_file();

    let path = match files {
        Some(path) => path,
        None => return 0 as *const c_char,
    };
    let path = match path.to_str() {
        Some(path) => path,
        None => return 0 as *const c_char,
    };
    let c_str = match CString::new(path) {
        Ok(c_str) => c_str,
        Err(_) => return 0 as *const c_char,
    };
    let pntr = c_str.into_raw();
    unsafe {
        STRING_POINTER = pntr;
    }
    return pntr;
}

fn parse_file_types(file_types: *const c_char) -> Vec<&'static str> {
    let c_str = unsafe { CStr::from_ptr(file_types) };
    let file_types = match c_str.to_str() {
        Ok(file_types) => file_types,
        Err(_) => return Vec::new(),
    };
    let extensions = file_types.split(";").collect();
    return extensions;
}
