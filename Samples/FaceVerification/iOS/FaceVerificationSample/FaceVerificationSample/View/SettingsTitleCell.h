#import <UIKit/UIKit.h>
#import "SettingHandler.h"

@interface SettingsTitleCell : UICollectionViewCell {
    
}

@property (weak, nonatomic) IBOutlet UILabel *settingLabel;

+(float)heightForCell;
-(void)setupWithSettingHandler:(SettingHandler*)handler;

@end
