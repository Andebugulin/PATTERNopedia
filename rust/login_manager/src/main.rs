// singleton pattern using lazy_static crate in Rust

use std::sync::Mutex;

#[derive(Debug)]
struct User {
    username: String,
    password: String,
}

struct LoginManager {
    logged_in_user: Option<User>,
}

impl LoginManager {
    fn new() -> Self {
        LoginManager { logged_in_user: None }
    }

    fn login(&mut self, username: &str, password: &str) {
        if self.is_logged_in() {
            let user = self.logged_in_user.as_ref().unwrap();
            println!("User {} with password {} is already logged in", user.username, user.password);
            return;
        }
        if username == "admin" && password == "password" {
            println!("User {} logged in successfully", username);
            self.logged_in_user = Some(User { username: username.to_string(), password: password.to_string() });
        } else {
            println!("Invalid username or password");
        }
    }

    fn logout(&mut self) {
        if let Some(user) = self.logged_in_user.take() {
            println!("User {} with password {} logged out successfully", user.username, user.password);
        } else {
            println!("No user is currently logged in");
        }
    }

    fn is_logged_in(&self) -> bool {
        self.logged_in_user.is_some()
    }

    fn get_logged_in_user(&self) -> Option<&User> {
        let current_user = self.logged_in_user.as_ref();
        match current_user {
            Some(user) => {
                println!("Current logged in user: {} with password {}", user.username, user.password);
                current_user
            }
            None => {
                println!("No user is currently logged in");
                None
            }
        }
    }
}

lazy_static::lazy_static! {
    static ref LOGIN_MANAGER: Mutex<LoginManager> = Mutex::new(LoginManager::new());
}

fn main() {
    let mut login_manager = LOGIN_MANAGER.lock().unwrap(); 
    login_manager.login("admin", "password");
    login_manager.get_logged_in_user();
    login_manager.logout();
    login_manager.get_logged_in_user();

    let mut login_manager2 = LOGIN_MANAGER.lock().unwrap();
    login_manager2.login("admin", "password");
    login_manager2.get_logged_in_user();
}
