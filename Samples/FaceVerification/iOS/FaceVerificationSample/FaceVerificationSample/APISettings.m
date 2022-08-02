#import "APISettings.h"

@implementation APISettingsController
@synthesize urlField, tokenField;

- (Boolean)keyExists:(NSString*)key {
    NSUserDefaults *defaults= [NSUserDefaults standardUserDefaults];
    if([[[defaults dictionaryRepresentation] allKeys] containsObject:key]){
        return true;
    }
    return false;
}

- (void)viewDidLoad {
    NSUserDefaults *userDefaults = [NSUserDefaults standardUserDefaults];
    if (![self keyExists:@"token"]) {
        [userDefaults setObject:@"9tlitadjedrg1emf9e27d0dlkt" forKey:@"token"];
    }
    [tokenField setText:[userDefaults objectForKey:@"token"]];
    
    if (![self keyExists:@"api"]) {
        [userDefaults setObject:@"https://faceverification.neurotechnology.com/rs/" forKey:@"api"];
    }
    [urlField setText:[userDefaults objectForKey:@"api"]];
}

- (IBAction)updateUrl:(id)sender {
    NSUserDefaults *userDefaults = [NSUserDefaults standardUserDefaults];
    [userDefaults setObject:[urlField text] forKey:@"api"];
}

- (IBAction)updateToken:(id)sender {
    NSUserDefaults *userDefaults = [NSUserDefaults standardUserDefaults];
    [userDefaults setObject:[tokenField text] forKey:@"token"];
}

- (BOOL)textFieldShouldReturn:(UITextField *)textField {
    [textField resignFirstResponder];
    return true;
}
- (IBAction)back:(id)sender {
    [self dismissViewControllerAnimated:YES completion:nil];
}

@end
