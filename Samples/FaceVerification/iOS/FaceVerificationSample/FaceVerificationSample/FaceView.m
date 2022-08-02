#import "FaceView.h"
#import "FaceOverlayView.h"

@implementation FaceView {
    UIImageView *imageView;
    FaceOverlayView *overlayView;
}

- (instancetype)initWithCoder:(NSCoder *)coder
{
    self = [super initWithCoder:coder];
    if (self) {
        imageView = [[UIImageView alloc] initWithImage:nil];
        imageView.contentMode = UIViewContentModeScaleAspectFit;
        imageView.transform = CGAffineTransformScale(imageView.transform, -1.0, 1.0);
        [self addSubview:imageView];
        overlayView = [[FaceOverlayView alloc] initWithFrame:self.bounds];
        [self addSubview:overlayView];
    }
    return self;
}

-(void)layoutSubviews {
    imageView.frame = self.bounds;
    overlayView.frame = self.bounds;
    [overlayView setNeedsDisplay];
    [super layoutSubviews];
}

- (void)setFaceImage:(UIImage*)image {
    [imageView setImage:image];
    overlayView->imageSize = image.size;
    overlayView->imageScale = image.scale;
}

- (void)setCurrentYaw:(NFloat)yaw {
    overlayView->currentYaw = yaw;
}

- (void)setCurrentRoll:(NFloat)roll {
    overlayView->currentRoll = roll;
}

- (void)setFaceBoundingRect:(CGRect)rect {
    overlayView->faceBoundingRect = rect;
}

- (void)setLivenessAction:(NfvcLivenessAction)action {
    overlayView->livenessAction = action;
}

- (void)setLivenessTargetYaw:(NFloat)yaw {
    overlayView->livenessTargetYaw = yaw;
}

- (void)setLivenessScore:(NByte)score {
    overlayView->livenessScore = score;
}

- (void)setIcaoWarnings:(NfvcIcaoWarnings)icaoWarnings {
    overlayView->icaoWarnings = icaoWarnings;
}

- (void)repaintOverlay {
    [overlayView setNeedsDisplay];
}

- (void)clearOverlay {
    [overlayView clear];
    [overlayView setNeedsDisplay];
}

@end
