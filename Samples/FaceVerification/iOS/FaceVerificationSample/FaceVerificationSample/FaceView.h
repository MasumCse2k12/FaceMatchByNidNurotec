#import <UIKit/UIKit.h>
#import <NFaceVerificationClient/NFaceVerificationClient.h>

@interface FaceView : UIView

- (void)setFaceImage:(UIImage*)image;
- (void)setCurrentYaw:(NFloat)yaw;
- (void)setCurrentRoll:(NFloat)roll;
- (void)setFaceBoundingRect:(CGRect)rect;
- (void)setLivenessAction:(NfvcLivenessAction)action;
- (void)setLivenessTargetYaw:(NFloat)yaw;
- (void)setLivenessScore:(NByte)score;
- (void)setIcaoWarnings:(NfvcIcaoWarnings)icaoWarnings;
- (void)repaintOverlay;
- (void)clearOverlay;

@end

