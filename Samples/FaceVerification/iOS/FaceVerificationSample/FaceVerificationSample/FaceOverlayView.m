// This is a private class of FaceView. Please, do not use it directly, use FaceView instead.

#import "FaceOverlayView.h"

@implementation FaceOverlayView

- (instancetype)initWithFrame:(CGRect)frame
{
    self = [super initWithFrame:frame];
    if (self) {
        self.backgroundColor = [UIColor clearColor];
        [self clear];
    }
    return self;
}

- (NSString*)getIcaoText:(NfvcIcaoWarnings)icaoWarnings {
    if (icaoWarnings & nfvciwFaceNotDetected) {
        return @"Face not detected";
    }
    if (icaoWarnings & nfvciwExpression) {
        return @"Expression";
    }
    if (icaoWarnings & nfvciwDarkGlasses) {
        return @"Dark glasses";
    }
    if (icaoWarnings & nfvciwBlink) {
        return @"Blink";
    }
    if (icaoWarnings & nfvciwMouthOpen) {
        return @"Mouth open";
    }
    if (icaoWarnings & nfvciwLookingAway) {
        return @"Looking away";
    }
    if (icaoWarnings & nfvciwRedEye) {
        return @"Red eye";
    }
    if (icaoWarnings & nfvciwFaceDarkness) {
        return @"Face darkness";
    }
    if (icaoWarnings & nfvciwUnnaturalSkinTone) {
        return @"Unnatural skin tone";
    }
    if (icaoWarnings & nfvciwWashedOut) {
        return @"Colors washed out";
    }
    if (icaoWarnings & nfvciwPixelation) {
        return @"Pixelation";
    }
    if (icaoWarnings & nfvciwSkinReflection) {
        return @"Skin reflection";
    }
    if (icaoWarnings & nfvciwGlassesReflection) {
        return @"Glasses reflection";
    }
    if (icaoWarnings & nfvciwRollLeft) {
        return @"Roll left";
    }
    if (icaoWarnings & nfvciwRollRight) {
        return @"Roll right";
    }
    if (icaoWarnings & nfvciwYawLeft) {
        return @"Yaw left";
    }
    if (icaoWarnings & nfvciwYawRight) {
        return @"Yaw right";
    }
    if (icaoWarnings & nfvciwPitchUp) {
        return @"Pitch up";
    }
    if (icaoWarnings & nfvciwPitchDown) {
        return @"Pitch down";
    }
    if (icaoWarnings & nfvciwTooNear) {
        return @"Too near";
    }
    if (icaoWarnings & nfvciwTooFar) {
        return @"Too far";
    }
    if (icaoWarnings & nfvciwTooNorth) {
        return @"Too north";
    }
    if (icaoWarnings & nfvciwTooSouth) {
        return @"Too south";
    }
    if (icaoWarnings & nfvciwTooWest) {
        return @"Too west";
    }
    if (icaoWarnings & nfvciwTooEast) {
        return @"Too east";
    }
    if (icaoWarnings & nfvciwSharpness) {
        return @"Sharpness";
    }
    if (icaoWarnings & nfvciwGrayscaleDensity) {
        return @"Grayscale density";
    }
    if (icaoWarnings & nfvciwSaturation) {
        return @"Saturation";
    }
    if (icaoWarnings & nfvciwBackgroundUniformity) {
        return @"Background uniformity";
    }
    return @"";
}

- (void)drawIcaoPitch:(Boolean)flip {
    UIBezierPath *path = [self getPitchPath];
    CGRect pitchPathBounds = CGPathGetPathBoundingBox(path.CGPath);
    CGRect fRect = self->faceBoundingRect;
    CGFloat scale = self.bounds.size.width / 5 / pitchPathBounds.size.width  * (fRect.size.width / self.bounds.size.width);
    [path applyTransform:CGAffineTransformMakeScale(scale, scale)];
    if (flip) {
        [path applyTransform:CGAffineTransformMakeScale(1, -1)];
        [path applyTransform:CGAffineTransformMakeTranslation(0, pitchPathBounds.size.height * scale)];
    } else {
        [path applyTransform:CGAffineTransformMakeTranslation(0, fRect.size.height)];
    }
    [path applyTransform:CGAffineTransformMakeTranslation(fRect.origin.x + fRect.size.width / 2, fRect.origin.y)];
    CGFloat centerX = (pitchPathBounds.origin.x + pitchPathBounds.size.width) / 2 * scale;
    CGFloat centerY = (pitchPathBounds.origin.y + pitchPathBounds.size.height) / 2 * scale;
    [path applyTransform:CGAffineTransformMakeTranslation(-centerX, -centerY)];
    
    [path applyTransform:CGAffineTransformMakeTranslation(-(fRect.origin.x + fRect.size.width / 2), -(fRect.origin.y + fRect.size.height / 2))];
    [path applyTransform:CGAffineTransformMakeRotation(-currentRoll / 180 * 3.14)];
    [path applyTransform:CGAffineTransformMakeTranslation((fRect.origin.x + fRect.size.width / 2), (fRect.origin.y + fRect.size.height / 2))];
    [[UIColor redColor] setStroke];
    [path setLineWidth:3];
    [path stroke];
}

- (void)drawIcaoMove:(float)rotate {
    UIBezierPath *path = [self getMovePath];
    CGRect movePathBounds = CGPathGetPathBoundingBox(path.CGPath);
    CGRect fRect = self->faceBoundingRect;
    CGFloat scale = self.bounds.size.width / 5 / movePathBounds.size.width  * (fRect.size.width / self.bounds.size.width);
    [path applyTransform:CGAffineTransformMakeScale(scale, scale)];
    [path applyTransform:CGAffineTransformMakeTranslation(movePathBounds.size.width * scale + fRect.size.width / 2, -movePathBounds.size.height * scale / 2)];
    [path applyTransform:CGAffineTransformMakeRotation(rotate / 180 * 3.14)];
    [path applyTransform:CGAffineTransformMakeTranslation(fRect.origin.x + fRect.size.width / 2, fRect.origin.y + fRect.size.height / 2)];
    
    [path applyTransform:CGAffineTransformMakeTranslation(-(fRect.origin.x + fRect.size.width / 2), -(fRect.origin.y + fRect.size.height / 2))];
    [path applyTransform:CGAffineTransformMakeRotation(-currentRoll / 180 * 3.14)];
    [path applyTransform:CGAffineTransformMakeTranslation((fRect.origin.x + fRect.size.width / 2), (fRect.origin.y + fRect.size.height / 2))];
    [[UIColor redColor] setStroke];
    [path setLineWidth:3];
    [path stroke];
}

- (void)drawIcaoRoll:(Boolean)flip {
    UIBezierPath *path = [self getRollPath];
    CGRect rollPathBounds = CGPathGetPathBoundingBox(path.CGPath);
    CGRect fRect = self->faceBoundingRect;
    CGFloat scale = self.bounds.size.width / 5 / rollPathBounds.size.width  * (fRect.size.width / self.bounds.size.width);
    [path applyTransform:CGAffineTransformMakeScale(scale, scale)];
    if (flip) {
        [path applyTransform:CGAffineTransformMakeScale(-1, 1)];
        [path applyTransform:CGAffineTransformMakeTranslation(rollPathBounds.size.width * scale + fRect.size.width, 0)];
    }
    [path applyTransform:CGAffineTransformMakeTranslation(fRect.origin.x, fRect.origin.y)];
    CGFloat centerX = (rollPathBounds.origin.x + rollPathBounds.size.width) / 2 * scale;
    CGFloat centerY = (rollPathBounds.origin.y + rollPathBounds.size.height) / 2 * scale;
    [path applyTransform:CGAffineTransformMakeTranslation(-centerX, -centerY)];
    
    [path applyTransform:CGAffineTransformMakeTranslation(-(fRect.origin.x + fRect.size.width / 2), -(fRect.origin.y + fRect.size.height / 2))];
    [path applyTransform:CGAffineTransformMakeRotation(-currentRoll / 180 * 3.14)];
    [path applyTransform:CGAffineTransformMakeTranslation((fRect.origin.x + fRect.size.width / 2), (fRect.origin.y + fRect.size.height / 2))];
    [[UIColor redColor] setStroke];
    [path setLineWidth:3];
    [path stroke];
}

- (void)drawIcaoYaw:(Boolean)flip {
    UIBezierPath *path = [self getYawPath];
    CGRect yawPathBounds = CGPathGetPathBoundingBox(path.CGPath);
    CGRect fRect = self->faceBoundingRect;
    CGFloat scale = self.bounds.size.width / 5 / yawPathBounds.size.width  * (fRect.size.width / self.bounds.size.width);
    [path applyTransform:CGAffineTransformMakeScale(scale, scale)];
    
    if (flip) {
        [path applyTransform:CGAffineTransformMakeScale(-1, 1)];
        [path applyTransform:CGAffineTransformMakeTranslation(yawPathBounds.size.width * scale, 0)];
    } else {
        [path applyTransform:CGAffineTransformMakeTranslation(fRect.size.width, 0)];
    }
    [path applyTransform:CGAffineTransformMakeTranslation(fRect.origin.x + (fRect.size.width / 5 * currentYaw) / 45,  fRect.origin.y + fRect.size.height / 2)];
    CGFloat centerX = (yawPathBounds.origin.x + yawPathBounds.size.width) / 2 * scale;
    CGFloat centerY = (yawPathBounds.origin.y + yawPathBounds.size.height) / 2 * scale;
    [path applyTransform:CGAffineTransformMakeTranslation(-centerX, -centerY)];
    
    [path applyTransform:CGAffineTransformMakeTranslation(-(fRect.origin.x + fRect.size.width / 2), -(fRect.origin.y + fRect.size.height / 2))];
    [path applyTransform:CGAffineTransformMakeRotation(-currentRoll / 180 * 3.14)];
    [path applyTransform:CGAffineTransformMakeTranslation((fRect.origin.x + fRect.size.width / 2), (fRect.origin.y + fRect.size.height / 2))];
    [[UIColor redColor] setStroke];
    [path setLineWidth:3];
    [path stroke];
}

- (void)drawFaceBoundingRect {
    if (faceBoundingRect.size.width > 0 && faceBoundingRect.size.height > 0) {
        CGFloat imageScale = fminf(self.bounds.size.width/imageSize.width, self.bounds.size.height/imageSize.height);
        CGFloat y = faceBoundingRect.origin.y * imageScale / self->imageScale;
        CGFloat width = faceBoundingRect.size.width * imageScale / self->imageScale;
        CGFloat height = faceBoundingRect.size.height * imageScale / self->imageScale;
        CGFloat xOffset = (self.bounds.size.width - (imageSize.width * imageScale)) * 0.5;
        CGFloat yOffset = (self.bounds.size.height - (imageSize.height * imageScale)) * 0.5;
        CGFloat x = self.bounds.size.width - xOffset - (faceBoundingRect.size.width + faceBoundingRect.origin.x) * imageScale / self->imageScale;
        CGRect fRect = CGRectMake(x, yOffset + y, width, height);
        
        UIBezierPath *path = [UIBezierPath bezierPath];
        [path moveToPoint:CGPointMake(fRect.origin.x, fRect.origin.y)];
        [path addLineToPoint:CGPointMake(fRect.origin.x + fRect.size.width, fRect.origin.y)];
        if (currentYaw > 0) {
            [path addLineToPoint:CGPointMake(fRect.origin.x + fRect.size.width + (fRect.size.width / 5 * currentYaw) / 45, fRect.origin.y + fRect.size.height / 2)];
        }
        [path addLineToPoint:CGPointMake(fRect.origin.x + fRect.size.width, fRect.origin.y + fRect.size.height)];
        [path addLineToPoint:CGPointMake(fRect.origin.x, fRect.origin.y + fRect.size.height)];
        if (currentYaw < 0) {
            [path addLineToPoint:CGPointMake(fRect.origin.x + (fRect.size.width / 5 * currentYaw) / 45, fRect.origin.y + fRect.size.height / 2)];
        }
        [path addLineToPoint:CGPointMake(fRect.origin.x, fRect.origin.y)];
        [path addLineToPoint:CGPointMake(fRect.origin.x + fRect.size.width, fRect.origin.y)];

        [path applyTransform:CGAffineTransformMakeTranslation(-(fRect.origin.x + fRect.size.width / 2), -(fRect.origin.y + fRect.size.height / 2))];
        [path applyTransform:CGAffineTransformMakeRotation(-currentRoll / 180 * 3.14)];
        [path applyTransform:CGAffineTransformMakeTranslation((fRect.origin.x + fRect.size.width / 2), (fRect.origin.y + fRect.size.height / 2))];
        self->faceBoundingRect = fRect;
        [[UIColor greenColor] setStroke];
        [path setLineWidth:3];
        [path stroke];
    }
}

- (void)drawText:(NSString*)text {
    NSMutableParagraphStyle *paragraphStyle = [[NSParagraphStyle defaultParagraphStyle] mutableCopy];
    paragraphStyle.alignment = NSTextAlignmentCenter;
    NSDictionary *attributes = @{
                                 NSFontAttributeName: [UIFont systemFontOfSize:16.0],
                                 NSParagraphStyleAttributeName: paragraphStyle,
                                 NSForegroundColorAttributeName: [UIColor blackColor],
                                 NSBackgroundColorAttributeName: [UIColor yellowColor]
                                 };
    CGFloat bottomOffset = 30.0;
    [text drawInRect:CGRectMake(self.bounds.origin.x, self.bounds.size.height-bottomOffset, self.bounds.size.width, bottomOffset) withAttributes:attributes];
}

- (void)drawIcaoWarningText:(NSString*)text {
    NSMutableParagraphStyle *paragraphStyle = [[NSParagraphStyle defaultParagraphStyle] mutableCopy];
    paragraphStyle.alignment = NSTextAlignmentLeft;
    NSDictionary *attributes = @{
                                 NSFontAttributeName: [UIFont systemFontOfSize:16.0],
                                 NSParagraphStyleAttributeName: paragraphStyle,
                                 NSForegroundColorAttributeName: [UIColor redColor],
                                 NSBackgroundColorAttributeName: [UIColor colorWithWhite:0 alpha:0]
                                 };
    CGFloat bottomOffset = 30.0;
    [text drawInRect:CGRectMake(self.bounds.origin.x, self.bounds.origin.y, self.bounds.size.width, bottomOffset) withAttributes:attributes];
}

- (CGFloat)calcYawOffset:(NFloat)yaw {
    CGFloat scale = self.bounds.size.width/110.0;
    return yaw*scale;
}

- (void)drawBlink {
    [[UIColor yellowColor] setFill];
    UIBezierPath *path = [self getBlinkPath];
    [path applyTransform:CGAffineTransformMakeScale(0.25, 0.25)];
    CGFloat tx = self.bounds.size.width*0.5 - path.bounds.size.width;
    CGFloat ty = self.bounds.size.height - 70.0 - path.bounds.size.height*0.5;
    CGFloat xOffset = [self calcYawOffset:currentYaw];
    [path applyTransform:CGAffineTransformMakeTranslation(tx + xOffset, ty)];
    [path fill];
}

- (void)drawTarget {
    [[UIColor yellowColor] setStroke];
    UIBezierPath *path = [self getTargetPath];
    [path setLineWidth:1.5];
    [path applyTransform:CGAffineTransformMakeScale(0.4, 0.4)];
    CGFloat tx = (self.bounds.size.width - path.bounds.size.width) * 0.5;
    CGFloat ty = self.bounds.size.height - 70.0;
    CGFloat xOffset = [self calcYawOffset:livenessTargetYaw];
    [path applyTransform:CGAffineTransformMakeTranslation(tx + xOffset, ty)];
    [path stroke];
}

- (void)drawArrow {
    [[UIColor yellowColor] setFill];
    UIBezierPath *path = [self getArrowPath];
    CGFloat tx = 0.0;
    if (livenessTargetYaw < currentYaw) {
        [path applyTransform:CGAffineTransformMakeScale(-0.1, 0.1)];
        tx = self.bounds.size.width*0.5 + path.bounds.size.width*0.75;
    } else {
        [path applyTransform:CGAffineTransformMakeScale(0.1, 0.1)];
        tx = self.bounds.size.width*0.5 - path.bounds.size.width*0.75;
    }
    CGFloat ty = self.bounds.size.height - 70.0;
    CGFloat xOffset = [self calcYawOffset:currentYaw];
    [path applyTransform:CGAffineTransformMakeTranslation(tx + xOffset, ty)];
    [path fill];
}

- (void)drawRect:(CGRect)rect {
    [super drawRect:rect];
    [self drawFaceBoundingRect];
    if (livenessAction & nfvclaRotateYaw) {
        if (livenessAction & nfvclaBlink) {
            [self drawBlink];
            [self drawText:@"Blink"];
        } else {
            [self drawTarget];
            [self drawArrow];
            [self drawText:[NSString stringWithFormat:@"Turn face on target"]];
        }
    } else if (livenessAction & nfvclaBlink) {
        [self drawText:@"Blink"];
    } else if (livenessAction & nfvclaKeepStill) {
        [self drawText:[NSString stringWithFormat:@"Keep still, score: %u", livenessScore]];
    } else if (livenessAction & nfvclaKeepRotatingYaw) {
        [self drawText:@"Turn face from side to side"];
    } else if (livenessAction & nfvclaTurnUp) {
        [self drawText:@"Turn face up"];
    } else if (livenessAction & nfvclaTurnDown) {
        [self drawText:@"Turn face down"];
    } else if (livenessAction & nfvclaTurnLeft) {
        [self drawText:@"Turn face left"];
    } else if (livenessAction & nfvclaTurnRight) {
        [self drawText:@"Turn face right"];
    } else if (livenessAction & nfvclaTurnToCenter) {
        [self drawText:@"Turn face to center"];
    }
    [self drawIcaoWarningText:[self getIcaoText:icaoWarnings]];
    // Draw icao warnings
    if ((icaoWarnings & nfvciwRollLeft) || (icaoWarnings & nfvciwRollRight)) {
        [self drawIcaoRoll:!(icaoWarnings & nfvciwRollLeft)];
    }
    if ((icaoWarnings & nfvciwYawLeft) || (icaoWarnings & nfvciwYawRight)) {
        if (((icaoWarnings & nfvciwYawLeft) && !(icaoWarnings & nfvciwTooEast)) ||
            ((icaoWarnings & nfvciwYawRight) && !(icaoWarnings & nfvciwTooWest)) ||
            (icaoWarnings & nfvciwTooNear)) {
            [self drawIcaoYaw:(icaoWarnings & nfvciwYawLeft)];
        }
    }
    if ((icaoWarnings & nfvciwTooSouth) || (icaoWarnings & nfvciwTooNorth) ||
        (icaoWarnings & nfvciwTooEast) || (icaoWarnings & nfvciwTooWest)) {
        if (icaoWarnings & nfvciwTooWest) {
            [self drawIcaoMove:0];
        }
        if (icaoWarnings & nfvciwTooEast) {
            [self drawIcaoMove:180];
        }
        if (icaoWarnings & nfvciwTooNorth) {
            [self drawIcaoMove:90];
        }
        if (icaoWarnings & nfvciwTooSouth) {
            [self drawIcaoMove:270];
        }
    }
    if ((icaoWarnings & nfvciwPitchDown) || (icaoWarnings & nfvciwPitchUp)) {
        if (((icaoWarnings & nfvciwPitchDown) && !(icaoWarnings & nfvciwTooSouth)) ||
            ((icaoWarnings & nfvciwPitchUp) && !(icaoWarnings & nfvciwTooNorth)) ||
            (icaoWarnings & nfvciwTooNear)) {
            [self drawIcaoPitch:!(icaoWarnings & nfvciwPitchDown)];
        }
    }
}

- (void)clear {
    imageSize = (CGSize){0};
    currentYaw = 0.0;
    faceBoundingRect = (CGRect){0};
    livenessAction = nfvclaNone;
    icaoWarnings = nfvciwNone;
    livenessTargetYaw = 0;
    livenessScore = 0;
}

- (UIBezierPath*)getArrowPath {
    UIBezierPath *path = [UIBezierPath bezierPath];
    [path moveToPoint:CGPointMake(32.0, 322.0)];
    [path addLineToPoint:CGPointMake(31.0, 315.0)];
    [path addLineToPoint:CGPointMake(29.0, 308.0)];
    [path addLineToPoint:CGPointMake(34.0, 302.0)];
    [path addLineToPoint:CGPointMake(63.0, 259.0)];
    [path addLineToPoint:CGPointMake(92.0, 216.0)];
    [path addLineToPoint:CGPointMake(120.0, 173.0)];
    [path addLineToPoint:CGPointMake(90.0, 129.0)];
    [path addLineToPoint:CGPointMake(61.0, 84.0)];
    [path addLineToPoint:CGPointMake(31.0, 39.0)];
    [path addLineToPoint:CGPointMake(32.0, 35.0)];
    [path addLineToPoint:CGPointMake(30.0, 28.0)];
    [path addLineToPoint:CGPointMake(33.0, 25.0)];
    [path addLineToPoint:CGPointMake(40.0, 25.0)];
    [path addLineToPoint:CGPointMake(47.0, 22.0)];
    [path addLineToPoint:CGPointMake(53.0, 27.0)];
    [path addLineToPoint:CGPointMake(145.0, 73.0)];
    [path addLineToPoint:CGPointMake(236.0, 118.0)];
    [path addLineToPoint:CGPointMake(327.0, 165.0)];
    [path addLineToPoint:CGPointMake(337.0, 181.0)];
    [path addLineToPoint:CGPointMake(317.0, 187.0)];
    [path addLineToPoint:CGPointMake(306.0, 192.0)];
    [path addLineToPoint:CGPointMake(220.0, 236.0)];
    [path addLineToPoint:CGPointMake(134.0, 279.0)];
    [path addLineToPoint:CGPointMake(47.0, 322.0)];
    [path addLineToPoint:CGPointMake(42.0, 322.0)];
    [path addLineToPoint:CGPointMake(37.0, 323.0)];
    [path closePath];
    return path;
}

- (UIBezierPath*)getBlinkPath {
    UIBezierPath *path = [UIBezierPath bezierPath];
    [path moveToPoint:CGPointMake(135.0, 129.0)];
    [path addLineToPoint:CGPointMake(135.0, 126.0)];
    [path addLineToPoint:CGPointMake(135.0, 122.0)];
    [path addLineToPoint:CGPointMake(135.0, 119.0)];
    [path addLineToPoint:CGPointMake(131.0, 119.0)];
    [path addLineToPoint:CGPointMake(127.0, 118.0)];
    [path addLineToPoint:CGPointMake(122.0, 118.0)];
    [path addLineToPoint:CGPointMake(120.0, 123.0)];
    [path addLineToPoint:CGPointMake(119.0, 129.0)];
    [path addLineToPoint:CGPointMake(115.0, 133.0)];
    [path addLineToPoint:CGPointMake(111.0, 132.0)];
    [path addLineToPoint:CGPointMake(102.0, 132.0)];
    [path addLineToPoint:CGPointMake(103.0, 126.0)];
    [path addLineToPoint:CGPointMake(104.0, 122.0)];
    [path addLineToPoint:CGPointMake(107.0, 117.0)];
    [path addLineToPoint:CGPointMake(107.0, 112.0)];
    [path addLineToPoint:CGPointMake(103.0, 110.0)];
    [path addLineToPoint:CGPointMake(100.0, 108.0)];
    [path addLineToPoint:CGPointMake(97.0, 106.0)];
    [path addLineToPoint:CGPointMake(93.0, 110.0)];
    [path addLineToPoint:CGPointMake(90.0, 115.0)];
    [path addLineToPoint:CGPointMake(85.0, 118.0)];
    [path addLineToPoint:CGPointMake(81.0, 116.0)];
    [path addLineToPoint:CGPointMake(71.0, 111.0)];
    [path addLineToPoint:CGPointMake(77.0, 106.0)];
    [path addLineToPoint:CGPointMake(79.0, 102.0)];
    [path addLineToPoint:CGPointMake(87.0, 97.0)];
    [path addLineToPoint:CGPointMake(81.0, 92.0)];
    [path addLineToPoint:CGPointMake(79.0, 88.0)];
    [path addLineToPoint:CGPointMake(73.0, 85.0)];
    [path addLineToPoint:CGPointMake(74.0, 80.0)];
    [path addLineToPoint:CGPointMake(78.0, 76.0)];
    [path addLineToPoint:CGPointMake(84.0, 73.0)];
    [path addLineToPoint:CGPointMake(90.0, 75.0)];
    [path addLineToPoint:CGPointMake(97.0, 78.0)];
    [path addLineToPoint:CGPointMake(101.0, 85.0)];
    [path addLineToPoint:CGPointMake(108.0, 90.0)];
    [path addLineToPoint:CGPointMake(124.0, 101.0)];
    [path addLineToPoint:CGPointMake(147.0, 103.0)];
    [path addLineToPoint:CGPointMake(166.0, 96.0)];
    [path addLineToPoint:CGPointMake(176.0, 92.0)];
    [path addLineToPoint:CGPointMake(183.0, 85.0)];
    [path addLineToPoint:CGPointMake(191.0, 78.0)];
    [path addLineToPoint:CGPointMake(195.0, 74.0)];
    [path addLineToPoint:CGPointMake(201.0, 73.0)];
    [path addLineToPoint:CGPointMake(205.0, 76.0)];
    [path addLineToPoint:CGPointMake(210.0, 78.0)];
    [path addLineToPoint:CGPointMake(213.0, 82.0)];
    [path addLineToPoint:CGPointMake(208.0, 86.0)];
    [path addLineToPoint:CGPointMake(206.0, 89.0)];
    [path addLineToPoint:CGPointMake(203.0, 93.0)];
    [path addLineToPoint:CGPointMake(200.0, 96.0)];
    [path addLineToPoint:CGPointMake(204.0, 101.0)];
    [path addLineToPoint:CGPointMake(208.0, 105.0)];
    [path addLineToPoint:CGPointMake(211.0, 110.0)];
    [path addLineToPoint:CGPointMake(208.0, 113.0)];
    [path addLineToPoint:CGPointMake(202.0, 123.0)];
    [path addLineToPoint:CGPointMake(198.0, 116.0)];
    [path addLineToPoint:CGPointMake(194.0, 113.0)];
    [path addLineToPoint:CGPointMake(192.0, 106.0)];
    [path addLineToPoint:CGPointMake(187.0, 107.0)];
    [path addLineToPoint:CGPointMake(182.0, 110.0)];
    [path addLineToPoint:CGPointMake(175.0, 113.0)];
    [path addLineToPoint:CGPointMake(180.0, 119.0)];
    [path addLineToPoint:CGPointMake(182.0, 123.0)];
    [path addLineToPoint:CGPointMake(186.0, 130.0)];
    [path addLineToPoint:CGPointMake(179.0, 132.0)];
    [path addLineToPoint:CGPointMake(175.0, 133.0)];
    [path addLineToPoint:CGPointMake(169.0, 136.0)];
    [path addLineToPoint:CGPointMake(168.0, 130.0)];
    [path addLineToPoint:CGPointMake(166.0, 126.0)];
    [path addLineToPoint:CGPointMake(166.0, 118.0)];
    [path addLineToPoint:CGPointMake(161.0, 118.0)];
    [path addLineToPoint:CGPointMake(158.0, 119.0)];
    [path addLineToPoint:CGPointMake(154.0, 119.0)];
    [path addLineToPoint:CGPointMake(150.0, 119.0)];
    [path addLineToPoint:CGPointMake(150.0, 125.0)];
    [path addLineToPoint:CGPointMake(150.0, 132.0)];
    [path addLineToPoint:CGPointMake(150.0, 138.0)];
    [path addLineToPoint:CGPointMake(145.0, 138.0)];
    [path addLineToPoint:CGPointMake(140.0, 138.0)];
    [path addLineToPoint:CGPointMake(135.0, 138.0)];
    [path addLineToPoint:CGPointMake(135.0, 135.0)];
    [path addLineToPoint:CGPointMake(135.0, 132.0)];
    [path closePath];
    return path;
}

- (UIBezierPath*)getTargetPath {
    UIBezierPath *path = [UIBezierPath bezierPath];
    [path addArcWithCenter:CGPointMake(40.0, 40.0) radius:40.0 startAngle:0.0 endAngle:2.0*M_PI clockwise:YES];
    [path moveToPoint:CGPointMake(70.0, 40.0)];
    [path addArcWithCenter:CGPointMake(40.0, 40.0) radius:30.0 startAngle:0.0 endAngle:2.0*M_PI clockwise:YES];
    [path moveToPoint:CGPointMake(60.0, 40.0)];
    [path addArcWithCenter:CGPointMake(40.0, 40.0) radius:20.0 startAngle:0.0 endAngle:2.0*M_PI clockwise:YES];
    [path moveToPoint:CGPointMake(50.0, 40.0)];
    [path addArcWithCenter:CGPointMake(40.0, 40.0) radius:10.0 startAngle:0.0 endAngle:2.0*M_PI clockwise:YES];
    return path;
}

- (UIBezierPath*)getYawPath {
    UIBezierPath *path = [UIBezierPath bezierPath];
    [path moveToPoint:CGPointMake(21.0, 102.0)];
    [path addLineToPoint:CGPointMake(14.0, 95.0)];
    [path addLineToPoint:CGPointMake(7.0, 89.0)];
    [path addLineToPoint:CGPointMake(1.0, 81.0)];
    [path addLineToPoint:CGPointMake(9.0, 70.0)];
    [path addLineToPoint:CGPointMake(20.0, 61.0)];
    [path addLineToPoint:CGPointMake(29.0, 51.0)];
    [path addLineToPoint:CGPointMake(33.0, 48.0)];
    [path addLineToPoint:CGPointMake(38.0, 41.0)];
    [path addLineToPoint:CGPointMake(42.0, 40.0)];
    [path addLineToPoint:CGPointMake(44.0, 47.0)];
    [path addLineToPoint:CGPointMake(43.0, 55.0)];
    [path addLineToPoint:CGPointMake(43.0, 62.0)];
    [path addLineToPoint:CGPointMake(64.0, 62.0)];
    [path addLineToPoint:CGPointMake(85.0, 59.0)];
    [path addLineToPoint:CGPointMake(106.0, 53.0)];
    [path addLineToPoint:CGPointMake(117.0, 50.0)];
    [path addLineToPoint:CGPointMake(128.0, 45.0)];
    [path addLineToPoint:CGPointMake(136.0, 36.0)];
    [path addLineToPoint:CGPointMake(142.0, 30.0)];
    [path addLineToPoint:CGPointMake(139.0, 44.0)];
    [path addLineToPoint:CGPointMake(140.0, 48.0)];
    [path addLineToPoint:CGPointMake(139.0, 57.0)];
    [path addLineToPoint:CGPointMake(140.0, 67.0)];
    [path addLineToPoint:CGPointMake(134.0, 73.0)];
    [path addLineToPoint:CGPointMake(126.0, 83.0)];
    [path addLineToPoint:CGPointMake(113.0, 89.0)];
    [path addLineToPoint:CGPointMake(101.0, 92.0)];
    [path addLineToPoint:CGPointMake(82.0, 98.0)];
    [path addLineToPoint:CGPointMake(63.0, 100.0)];
    [path addLineToPoint:CGPointMake(43.0, 101.0)];
    [path addLineToPoint:CGPointMake(43.0, 108.0)];
    [path addLineToPoint:CGPointMake(44.0, 115.0)];
    [path addLineToPoint:CGPointMake(42.0, 121.0)];
    [path addLineToPoint:CGPointMake(38.0, 120.0)];
    [path addLineToPoint:CGPointMake(33.0, 113.0)];
    [path addLineToPoint:CGPointMake(29.0, 110.0)];
    [path addLineToPoint:CGPointMake(26.0, 107.0)];
    [path addLineToPoint:CGPointMake(23.0, 105.0)];
    [path addLineToPoint:CGPointMake(21.0, 102.0)];
    
    [path moveToPoint:CGPointMake(120.0, 34.0)];
    [path addLineToPoint:CGPointMake(109.0, 26.0)];
    [path addLineToPoint:CGPointMake(96.0, 23.0)];
    [path addLineToPoint:CGPointMake(83.0, 21.0)];
    [path addLineToPoint:CGPointMake(83.0, 14.0)];
    [path addLineToPoint:CGPointMake(83.0, 7.0)];
    [path addLineToPoint:CGPointMake(83.0, 0.0)];
    [path addLineToPoint:CGPointMake(98.0, 2.0)];
    [path addLineToPoint:CGPointMake(114.0, 6.0)];
    [path addLineToPoint:CGPointMake(126.0, 14.0)];
    [path addLineToPoint:CGPointMake(132.0, 18.0)];
    [path addLineToPoint:CGPointMake(134.0, 27.0)];
    [path addLineToPoint:CGPointMake(130.0, 32.0)];
    [path addLineToPoint:CGPointMake(127.0, 36.0)];
    [path addLineToPoint:CGPointMake(124.0, 38.0)];
    [path addLineToPoint:CGPointMake(120.0, 34.0)];
    [path closePath];
    return path;
}

- (UIBezierPath*)getRollPath {
    UIBezierPath *path = [UIBezierPath bezierPath];
    [path moveToPoint:CGPointMake(10.0, 297.0)];
    [path addLineToPoint:CGPointMake(0.0, 301.0)];
    [path addLineToPoint:CGPointMake(2.0, 290.0)];
    [path addLineToPoint:CGPointMake(5.0, 282.0)];
    [path addLineToPoint:CGPointMake(24.0, 210.0)];
    [path addLineToPoint:CGPointMake(72.0, 147.0)];
    [path addLineToPoint:CGPointMake(134.0, 106.0)];
    [path addLineToPoint:CGPointMake(189.0, 68.0)];
    [path addLineToPoint:CGPointMake(254.0, 46.0)];
    [path addLineToPoint:CGPointMake(321.0, 42.0)];
    [path addLineToPoint:CGPointMake(329.0, 41.0)];
    [path addLineToPoint:CGPointMake(354.0, 43.0)];
    [path addLineToPoint:CGPointMake(333.0, 36.0)];
    [path addLineToPoint:CGPointMake(322.0, 27.0)];
    [path addLineToPoint:CGPointMake(295.0, 30.0)];
    [path addLineToPoint:CGPointMake(296.0, 11.0)];
    [path addLineToPoint:CGPointMake(293.0, 1.0)];
    [path addLineToPoint:CGPointMake(299.0, 3.0)];
    [path addLineToPoint:CGPointMake(306.0, 6.0)];
    [path addLineToPoint:CGPointMake(340.0, 21.0)];
    [path addLineToPoint:CGPointMake(375.0, 34.0)];
    [path addLineToPoint:CGPointMake(408.0, 49.0)];
    [path addLineToPoint:CGPointMake(373.0, 68.0)];
    [path addLineToPoint:CGPointMake(338.0, 87.0)];
    [path addLineToPoint:CGPointMake(302.0, 104.0)];
    [path addLineToPoint:CGPointMake(300.0, 99.0)];
    [path addLineToPoint:CGPointMake(297.0, 86.0)];
    [path addLineToPoint:CGPointMake(305.0, 84.0)];
    [path addLineToPoint:CGPointMake(320.0, 76.0)];
    [path addLineToPoint:CGPointMake(334.0, 69.0)];
    [path addLineToPoint:CGPointMake(349.0, 61.0)];
    [path addLineToPoint:CGPointMake(300.0, 61.0)];
    [path addLineToPoint:CGPointMake(250.0, 69.0)];
    [path addLineToPoint:CGPointMake(205.0, 89.0)];
    [path addLineToPoint:CGPointMake(118.0, 124.0)];
    [path addLineToPoint:CGPointMake(45.0, 199.0)];
    [path addLineToPoint:CGPointMake(22.0, 291.0)];
    [path addLineToPoint:CGPointMake(22.0, 299.0)];
    [path addLineToPoint:CGPointMake(16.0, 297.0)];
    [path addLineToPoint:CGPointMake(10.0, 297.0)];
    [path closePath];
    return path;
}

- (UIBezierPath*)getMovePath {
    UIBezierPath *path = [UIBezierPath bezierPath];
    [path moveToPoint:CGPointMake(90.0, 105.0)];
    [path addLineToPoint:CGPointMake(90.0, 100.0)];
    [path addLineToPoint:CGPointMake(90.0, 95.0)];
    [path addLineToPoint:CGPointMake(90.0, 90.0)];
    [path addLineToPoint:CGPointMake(60.0, 90.0)];
    [path addLineToPoint:CGPointMake(30.0, 90.0)];
    [path addLineToPoint:CGPointMake(0.0, 90.0)];
    [path addLineToPoint:CGPointMake(0.0, 70.0)];
    [path addLineToPoint:CGPointMake(0.0, 50.0)];
    [path addLineToPoint:CGPointMake(0.0, 30.0)];
    [path addLineToPoint:CGPointMake(30.0, 30.0)];
    [path addLineToPoint:CGPointMake(60.0, 30.0)];
    [path addLineToPoint:CGPointMake(90.0, 29.0)];
    [path addLineToPoint:CGPointMake(91.0, 20.0)];
    [path addLineToPoint:CGPointMake(91.0, 10.0)];
    [path addLineToPoint:CGPointMake(91.0, 0.0)];
    [path addLineToPoint:CGPointMake(121.0, 20.0)];
    [path addLineToPoint:CGPointMake(152.0, 39.0)];
    [path addLineToPoint:CGPointMake(182.0, 59.0)];
    [path addLineToPoint:CGPointMake(180.0, 64.0)];
    [path addLineToPoint:CGPointMake(170.0, 68.0)];
    [path addLineToPoint:CGPointMake(165.0, 72.0)];
    [path addLineToPoint:CGPointMake(140.0, 88.0)];
    [path addLineToPoint:CGPointMake(116.0, 104.0)];
    [path addLineToPoint:CGPointMake(91.0, 120.0)];
    [path addLineToPoint:CGPointMake(91.0, 115.0)];
    [path addLineToPoint:CGPointMake(91.0, 110.0)];
    [path addLineToPoint:CGPointMake(90.0, 105.0)];
    
    [path moveToPoint:CGPointMake(136.0, 88.0)];
    [path addLineToPoint:CGPointMake(150.0, 79.0)];
    [path addLineToPoint:CGPointMake(165.0, 71.0)];
    [path addLineToPoint:CGPointMake(178.0, 61.0)];
    [path addLineToPoint:CGPointMake(176.0, 55.0)];
    [path addLineToPoint:CGPointMake(166.0, 52.0)];
    [path addLineToPoint:CGPointMake(161.0, 48.0)];
    [path addLineToPoint:CGPointMake(138.0, 33.0)];
    [path addLineToPoint:CGPointMake(115.0, 18.0)];
    [path addLineToPoint:CGPointMake(92.0, 3.0)];
    [path addLineToPoint:CGPointMake(91.0, 12.0)];
    [path addLineToPoint:CGPointMake(92.0, 22.0)];
    [path addLineToPoint:CGPointMake(92.0, 31.0)];
    [path addLineToPoint:CGPointMake(62.0, 31.0)];
    [path addLineToPoint:CGPointMake(32.0, 31.0)];
    [path addLineToPoint:CGPointMake(2.0, 31.0)];
    [path addLineToPoint:CGPointMake(2.0, 50.0)];
    [path addLineToPoint:CGPointMake(2.0, 70.0)];
    [path addLineToPoint:CGPointMake(2.0, 89.0)];
    [path addLineToPoint:CGPointMake(32.0, 89.0)];
    [path addLineToPoint:CGPointMake(62.0, 89.0)];
    [path addLineToPoint:CGPointMake(92.0, 89.0)];
    [path addLineToPoint:CGPointMake(92.0, 98.0)];
    [path addLineToPoint:CGPointMake(91.0, 108.0)];
    [path addLineToPoint:CGPointMake(93.0, 117.0)];
    [path addLineToPoint:CGPointMake(107.0, 108.0)];
    [path addLineToPoint:CGPointMake(122.0, 98.0)];
    [path addLineToPoint:CGPointMake(136.0, 88.0)];
    [path closePath];
    return path;
}

- (UIBezierPath*)getPitchPath {
    UIBezierPath *path = [UIBezierPath bezierPath];
    [path moveToPoint:CGPointMake(92.0, 45.0)];
    [path addLineToPoint:CGPointMake(89.0, 45.0)];
    [path addLineToPoint:CGPointMake(87.0, 45.0)];
    [path addLineToPoint:CGPointMake(84.0, 45.0)];
    [path addLineToPoint:CGPointMake(83.0, 72.0)];
    [path addLineToPoint:CGPointMake(80.0, 100.0)];
    [path addLineToPoint:CGPointMake(72.0, 126.0)];
    [path addLineToPoint:CGPointMake(69.0, 134.0)];
    [path addLineToPoint:CGPointMake(64.0, 141.0)];
    [path addLineToPoint:CGPointMake(57.0, 147.0)];
    [path addLineToPoint:CGPointMake(50.0, 151.0)];
    [path addLineToPoint:CGPointMake(42.0, 149.0)];
    [path addLineToPoint:CGPointMake(34.0, 149.0)];
    [path addLineToPoint:CGPointMake(29.0, 150.0)];
    [path addLineToPoint:CGPointMake(27.0, 146.0)];
    [path addLineToPoint:CGPointMake(32.0, 144.0)];
    [path addLineToPoint:CGPointMake(41.0, 132.0)];
    [path addLineToPoint:CGPointMake(43.0, 117.0)];
    [path addLineToPoint:CGPointMake(46.0, 102.0)];
    [path addLineToPoint:CGPointMake(49.0, 84.0)];
    [path addLineToPoint:CGPointMake(51.0, 65.0)];
    [path addLineToPoint:CGPointMake(51.0, 45.0)];
    [path addLineToPoint:CGPointMake(46.0, 45.0)];
    [path addLineToPoint:CGPointMake(37.0, 47.0)];
    [path addLineToPoint:CGPointMake(34.0, 43.0)];
    [path addLineToPoint:CGPointMake(36.0, 37.0)];
    [path addLineToPoint:CGPointMake(42.0, 32.0)];
    [path addLineToPoint:CGPointMake(46.0, 26.0)];
    [path addLineToPoint:CGPointMake(53.0, 17.0)];
    [path addLineToPoint:CGPointMake(59.0, 8.0)];
    [path addLineToPoint:CGPointMake(67.0, 0.0)];
    [path addLineToPoint:CGPointMake(78.0, 12.0)];
    [path addLineToPoint:CGPointMake(88.0, 26.0)];
    [path addLineToPoint:CGPointMake(98.0, 40.0)];
    [path addLineToPoint:CGPointMake(103.0, 46.0)];
    [path addLineToPoint:CGPointMake(97.0, 45.0)];
    [path addLineToPoint:CGPointMake(92.0, 45.0)];
    
    [path moveToPoint:CGPointMake(51.0, 22.0)];
    [path addLineToPoint:CGPointMake(45.0, 30.0)];
    [path addLineToPoint:CGPointMake(40.0, 37.0)];
    [path addLineToPoint:CGPointMake(35.0, 44.0)];
    [path addLineToPoint:CGPointMake(40.0, 44.0)];
    [path addLineToPoint:CGPointMake(46.0, 44.0)];
    [path addLineToPoint:CGPointMake(52.0, 44.0)];
    [path addLineToPoint:CGPointMake(52.0, 72.0)];
    [path addLineToPoint:CGPointMake(49.0, 100.0)];
    [path addLineToPoint:CGPointMake(42.0, 127.0)];
    [path addLineToPoint:CGPointMake(40.0, 135.0)];
    [path addLineToPoint:CGPointMake(36.0, 143.0)];
    [path addLineToPoint:CGPointMake(30.0, 148.0)];
    [path addLineToPoint:CGPointMake(38.0, 148.0)];
    [path addLineToPoint:CGPointMake(47.0, 149.0)];
    [path addLineToPoint:CGPointMake(55.0, 146.0)];
    [path addLineToPoint:CGPointMake(65.0, 140.0)];
    [path addLineToPoint:CGPointMake(69.0, 128.0)];
    [path addLineToPoint:CGPointMake(73.0, 117.0)];
    [path addLineToPoint:CGPointMake(80.0, 93.0)];
    [path addLineToPoint:CGPointMake(81.0, 68.0)];
    [path addLineToPoint:CGPointMake(83.0, 44.0)];
    [path addLineToPoint:CGPointMake(88.0, 44.0)];
    [path addLineToPoint:CGPointMake(94.0, 44.0)];
    [path addLineToPoint:CGPointMake(99.0, 44.0)];
    [path addLineToPoint:CGPointMake(89.0, 30.0)];
    [path addLineToPoint:CGPointMake(78.0, 15.0)];
    [path addLineToPoint:CGPointMake(67.0, 1.0)];
    [path addLineToPoint:CGPointMake(62.0, 8.0)];
    [path addLineToPoint:CGPointMake(56.0, 15.0)];
    [path addLineToPoint:CGPointMake(51.0, 22.0)];
    
    [path moveToPoint:CGPointMake(28.0, 138.0)];
    [path addLineToPoint:CGPointMake(23.0, 146.0)];
    [path addLineToPoint:CGPointMake(12.0, 140.0)];
    [path addLineToPoint:CGPointMake(11.0, 133.0)];
    [path addLineToPoint:CGPointMake(4.0, 120.0)];
    [path addLineToPoint:CGPointMake(2.0, 106.0)];
    [path addLineToPoint:CGPointMake(1.0, 92.0)];
    [path addLineToPoint:CGPointMake(3.0, 86.0)];
    [path addLineToPoint:CGPointMake(13.0, 89.0)];
    [path addLineToPoint:CGPointMake(17.0, 90.0)];
    [path addLineToPoint:CGPointMake(20.0, 95.0)];
    [path addLineToPoint:CGPointMake(19.0, 102.0)];
    [path addLineToPoint:CGPointMake(22.0, 108.0)];
    [path addLineToPoint:CGPointMake(24.0, 116.0)];
    [path addLineToPoint:CGPointMake(27.0, 124.0)];
    [path addLineToPoint:CGPointMake(31.0, 132.0)];
    [path addLineToPoint:CGPointMake(3.0, 135.0)];
    [path addLineToPoint:CGPointMake(29.0, 137.0)];
    [path addLineToPoint:CGPointMake(28.0, 138.0)];
    
    [path moveToPoint:CGPointMake(27.0, 128.0)];
    [path addLineToPoint:CGPointMake(21.0, 117.0)];
    [path addLineToPoint:CGPointMake(20.0, 104.0)];
    [path addLineToPoint:CGPointMake(17.0, 92.0)];
    [path addLineToPoint:CGPointMake(13.0, 89.0)];
    [path addLineToPoint:CGPointMake(6.0, 90.0)];
    [path addLineToPoint:CGPointMake(2.0, 91.0)];
    [path addLineToPoint:CGPointMake(4.0, 106.0)];
    [path addLineToPoint:CGPointMake(7.0, 123.0)];
    [path addLineToPoint:CGPointMake(15.0, 137.0)];
    [path addLineToPoint:CGPointMake(18.0, 144.0)];
    [path addLineToPoint:CGPointMake(31.0, 139.0)];
    [path addLineToPoint:CGPointMake(29.0, 132.0)];
    [path addLineToPoint:CGPointMake(28.0, 130.0)];
    [path addLineToPoint:CGPointMake(27.0, 129.0)];
    [path addLineToPoint:CGPointMake(27.0, 128.0)];
    [path closePath];
    return path;
}

@end
