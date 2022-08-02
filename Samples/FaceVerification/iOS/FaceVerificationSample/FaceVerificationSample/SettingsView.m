#import "SettingsView.h"

@implementation SettingsView {
    
}
@synthesize tokenField, apiField, matchingThreshold, matchingThresholdLabel, collectionView;

- (CGSize)collectionView:(UICollectionView *)collectionView layout:(UICollectionViewLayout*)collectionViewLayout sizeForItemAtIndexPath:(NSIndexPath *)indexPath {
    SettingHandler *settingHandler = [settingHandlers objectAtIndex:indexPath.item];
    if ([settingHandler.type compare:@"slider"] == NSOrderedSame) {
        return CGSizeMake(collectionView.frame.size.width, [SettingsSliderCell heightForCell]);
    } else if ([settingHandler.type compare:@"switch"] == NSOrderedSame) {
        return CGSizeMake(collectionView.frame.size.width, [SettingsSwitchCell heightForCell]);
    } else if ([settingHandler.type compare:@"picker"] == NSOrderedSame) {
        return CGSizeMake(collectionView.frame.size.width, [SettingsPickerCell heightForCell]);
    } else {
        return CGSizeMake(collectionView.frame.size.width, [SettingsTitleCell heightForCell]);
    }
}

- (NSInteger)collectionView:(UICollectionView *)collectionView numberOfItemsInSection:(NSInteger)section {
    return settingHandlers.count;
}

- (UICollectionViewCell *)collectionView:(UICollectionView *)collectionViewArg cellForItemAtIndexPath:(NSIndexPath *)indexPath{
    SettingHandler *settingHandler = [settingHandlers objectAtIndex:indexPath.item];
    if ([settingHandler.type compare:@"slider"] == NSOrderedSame) {
        SettingsSliderCell *cell = [collectionViewArg dequeueReusableCellWithReuseIdentifier:@"SettingsSliderCell" forIndexPath:indexPath];
        [cell setupWithSettingHandler:settingHandler];
        return cell;
    } else if ([settingHandler.type compare:@"switch"] == NSOrderedSame) {
        SettingsSwitchCell *cell = [collectionViewArg dequeueReusableCellWithReuseIdentifier:@"SettingsSwitchCell" forIndexPath:indexPath];
        [cell setupWithSettingHandler:settingHandler];
        return cell;
    } else if ([settingHandler.type compare:@"picker"] == NSOrderedSame) {
        SettingsPickerCell *cell = [collectionViewArg dequeueReusableCellWithReuseIdentifier:@"SettingsPickerCell" forIndexPath:indexPath];
        [cell setupWithSettingHandler:settingHandler];
        return cell;
    } else {
        SettingsTitleCell *cell = [collectionViewArg dequeueReusableCellWithReuseIdentifier:@"SettingsTitleCell" forIndexPath:indexPath];
        [cell setupWithSettingHandler:settingHandler];
        return cell;
    }
}

- (void)viewDidLoad {
    [super viewDidLoad];

    collectionView.delegate = self;
    collectionView.dataSource = self;
    collectionView.alwaysBounceVertical = true;
    [collectionView registerNib:[UINib nibWithNibName:@"SettingsSliderCell" bundle:nil] forCellWithReuseIdentifier:@"SettingsSliderCell"];
    [collectionView registerNib:[UINib nibWithNibName:@"SettingsSwitchCell" bundle:nil] forCellWithReuseIdentifier:@"SettingsSwitchCell"];
    [collectionView registerNib:[UINib nibWithNibName:@"SettingsPickerCell" bundle:nil] forCellWithReuseIdentifier:@"SettingsPickerCell"];
    [collectionView registerNib:[UINib nibWithNibName:@"SettingsTitleCell" bundle:nil] forCellWithReuseIdentifier:@"SettingsTitleCell"];
    
    settingHandlers = [Settings getAllSettingHandlers];
}
- (IBAction)back:(id)sender {
    [self dismissViewControllerAnimated:YES completion:nil];
}

@end
