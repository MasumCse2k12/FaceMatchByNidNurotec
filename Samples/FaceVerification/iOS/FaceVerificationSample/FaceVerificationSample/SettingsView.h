#import <UIKit/UIKit.h>
#import "Settings.h"
#import "SettingHandler.h"
#import "SettingsSliderCell.h"
#import "SettingsSwitchCell.h"
#import "SettingsTitleCell.h"
#import "SettingsPickerCell.h"

@interface SettingsView : UIViewController<UICollectionViewDataSource, UICollectionViewDelegate, UICollectionViewDelegateFlowLayout> {
    NSArray *settingHandlers;
}
@property (weak, nonatomic) IBOutlet UITextField *tokenField;
@property (weak, nonatomic) IBOutlet UITextField *apiField;
@property (weak, nonatomic) IBOutlet UISlider *matchingThreshold;
@property (weak, nonatomic) IBOutlet UILabel *matchingThresholdLabel;
@property (nonatomic, retain) IBOutlet UICollectionView *collectionView;

@end
