// This is a private class of FaceView. Please, do not use it directly, use FaceView instead.

#import <UIKit/UIKit.h>
#import <NFaceVerificationClient/NFaceVerificationClient.h>

@interface FaceOverlayView : UIView {
    @public
    CGSize imageSize;
    CGFloat imageScale;
    NFloat currentYaw;
    NFloat currentRoll;
    CGRect faceBoundingRect;
    NfvcLivenessAction livenessAction;
    NFloat livenessTargetYaw;
    NByte livenessScore;
    NfvcIcaoWarnings icaoWarnings;
}

- (void)clear;

@end
