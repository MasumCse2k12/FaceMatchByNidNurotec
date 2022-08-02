#import <UIKit/UIKit.h>
#import "SettingHandler.h"

@interface SettingsSwitchCell : UICollectionViewCell {
    
}

@property (weak, nonatomic) IBOutlet UILabel *settingLabel;
@property (nonatomic, retain) SettingHandler *settingHandler;
@property (weak, nonatomic) IBOutlet UISwitch *uiSwitch;

+(float)heightForCell;
-(void)setupWithSettingHandler:(SettingHandler*)handler;

@end
