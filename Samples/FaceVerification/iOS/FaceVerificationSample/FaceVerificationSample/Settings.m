#import "Settings.h"

@implementation Settings

+ (Boolean)keyExists:(NSString*)key {
    NSUserDefaults *defaults= [NSUserDefaults standardUserDefaults];
    if([[[defaults dictionaryRepresentation] allKeys] containsObject:key]){
        return true;
    }
    return false;
}

+ (NSString*)getSettingString:(NSString*)setting {
    NSUserDefaults *userDefaults = [NSUserDefaults standardUserDefaults];
    return [userDefaults objectForKey:setting];
}

+ (void)updateSetting:(SettingHandler*)handler {
    NSUserDefaults *userDefaults = [NSUserDefaults standardUserDefaults];
    if ([handler.type compare:@"slider"] == NSOrderedSame) {
        NSString *key = handler.key;
        if (![self keyExists:key]) {
            [userDefaults setInteger:handler.defaultVal forKey:key];
        }
        NInt value = (int)[userDefaults integerForKey:key];
        if ([key compare:@"matching_threshold"] == NSOrderedSame) {
            NRESULT_CHECK(NFaceVerificationClientSetMatchingThreshold(value));
        } else if ([key compare:@"quality_threshold"] == NSOrderedSame) {
            NRESULT_CHECK(NFaceVerificationClientSetQualityThreshold(value));
        } else if ([key compare:@"liveness_threshold"] == NSOrderedSame) {
            NRESULT_CHECK(NFaceVerificationClientSetLivenessThreshold(value));
        } else if ([key compare:@"saturation_threshold"] == NSOrderedSame) {
            NRESULT_CHECK(NFaceVerificationClientSetIcaoWarningThreshold(nfvciwSaturation, value));
        } else if ([key compare:@"sharpness_threshold"] == NSOrderedSame) {
            NRESULT_CHECK(NFaceVerificationClientSetIcaoWarningThreshold(nfvciwSharpness, value));
        } else if ([key compare:@"uniformity_threshold"] == NSOrderedSame) {
            NRESULT_CHECK(NFaceVerificationClientSetIcaoWarningThreshold(nfvciwBackgroundUniformity, value));
        } else if ([key compare:@"gray_threshold"] == NSOrderedSame) {
            NRESULT_CHECK(NFaceVerificationClientSetIcaoWarningThreshold(nfvciwGrayscaleDensity, value));
        } else if ([key compare:@"lookaway_threshold"] == NSOrderedSame) {
            NRESULT_CHECK(NFaceVerificationClientSetIcaoWarningThreshold(nfvciwLookingAway, value));
        } else if ([key compare:@"redeye_threshold"] == NSOrderedSame) {
            NRESULT_CHECK(NFaceVerificationClientSetIcaoWarningThreshold(nfvciwRedEye, value));
        } else if ([key compare:@"facedark_threshold"] == NSOrderedSame) {
            NRESULT_CHECK(NFaceVerificationClientSetIcaoWarningThreshold(nfvciwFaceDarkness, value));
        } else if ([key compare:@"unnatural_threshold"] == NSOrderedSame) {
            NRESULT_CHECK(NFaceVerificationClientSetIcaoWarningThreshold(nfvciwUnnaturalSkinTone, value));
        } else if ([key compare:@"washed_threshold"] == NSOrderedSame) {
            NRESULT_CHECK(NFaceVerificationClientSetIcaoWarningThreshold(nfvciwWashedOut, value));
        } else if ([key compare:@"pixelation_threshold"] == NSOrderedSame) {
            NRESULT_CHECK(NFaceVerificationClientSetIcaoWarningThreshold(nfvciwPixelation, value));
        } else if ([key compare:@"skinrefl_threshold"] == NSOrderedSame) {
            NRESULT_CHECK(NFaceVerificationClientSetIcaoWarningThreshold(nfvciwSkinReflection, value));
        } else if ([key compare:@"glassrefl_threshold"] == NSOrderedSame) {
            NRESULT_CHECK(NFaceVerificationClientSetIcaoWarningThreshold(nfvciwGlassesReflection, value));
        } else if ([key compare:@"expression_threshold"] == NSOrderedSame) {
            NRESULT_CHECK(NFaceVerificationClientSetIcaoWarningThreshold(nfvciwExpression, value));
        } else if ([key compare:@"darkglass_threshold"] == NSOrderedSame) {
            NRESULT_CHECK(NFaceVerificationClientSetIcaoWarningThreshold(nfvciwDarkGlasses, value));
        } else if ([key compare:@"blink_threshold"] == NSOrderedSame) {
            NRESULT_CHECK(NFaceVerificationClientSetIcaoWarningThreshold(nfvciwBlink, value));
        } else if ([key compare:@"mouth_threshold"] == NSOrderedSame) {
            NRESULT_CHECK(NFaceVerificationClientSetIcaoWarningThreshold(nfvciwMouthOpen, value));
        }
    } else if ([handler.type compare:@"switch"] == NSOrderedSame) {
        NSString *key = handler.key;
        BOOL value = (int)[userDefaults boolForKey:key];
        if ([key compare:@"check_icao"] == NSOrderedSame) {
            NRESULT_CHECK(NFaceVerificationClientSetCheckIcaoCompliance(value));
        } else if ([key compare:@"manual_capturing"] == NSOrderedSame) {
            NRESULT_CHECK(NFaceVerificationClientSetUseManualCapturing(value));
        }
    } else if ([handler.type compare:@"picker"] == NSOrderedSame) {
        NSString *key = handler.key;
        NInt value = (int)[userDefaults integerForKey:key];
        if ([key compare:@"liveness_mode"] == NSOrderedSame) {
            switch (value) {
                case 0:
                    NRESULT_CHECK(NFaceVerificationClientSetLivenessMode(nfvclmNone));
                    break;
                case 1:
                    NRESULT_CHECK(NFaceVerificationClientSetLivenessMode(nfvclmPassive));
                    break;
                case 2:
                    NRESULT_CHECK(NFaceVerificationClientSetLivenessMode(nfvclmActive));
                    break;
                case 3:
                    NRESULT_CHECK(NFaceVerificationClientSetLivenessMode(nfvclmPassiveAndActive));
                    break;
                case 4:
                    NRESULT_CHECK(NFaceVerificationClientSetLivenessMode(nfvclmSimple));
                    break;
                case 5:
                    NRESULT_CHECK(NFaceVerificationClientSetLivenessMode(nfvclmCustom));
                    break;
                default:
                    break;
            }
        }
        if ([key compare:@"camera"] == NSOrderedSame) {
            NChar* cameraName;
            NInt cameraNameLength;
            NRESULT_CHECK(NFaceVerificationClientGetAvailableCamera(value, &cameraName, &cameraNameLength));
            NRESULT_CHECK(NFaceVerificationClientSetCurrentCamera(cameraName, cameraNameLength));
        }
    }
}

+ (void)initSettings {
    NSArray *handlers = [self getAllSettingHandlers];
    for (SettingHandler *handler in handlers) {
        [self updateSetting:handler];
    }
}

+ (NSArray*)getAllSettingHandlers {
    SettingHandler *verificationSettings = [[SettingHandler alloc] initWithTitle:@"Verification Settings" key:nil type:@"title"];
    SettingHandler *matchingThreshold = [[SettingHandler alloc] initWithTitle:@"Matching threshold" key:@"matching_threshold" type:@"slider"];
    
    SettingHandler *extractionSettings = [[SettingHandler alloc] initWithTitle:@"Extraction Settings" key:nil type:@"title"];
    SettingHandler *qualityThreshold = [[SettingHandler alloc] initWithTitle:@"Quality threshold" key:@"quality_threshold" type:@"slider"];
    
    SettingHandler *livenessSettings = [[SettingHandler alloc] initWithTitle:@"Liveness Settings" key:nil type:@"title"];
    SettingHandler *livenessThreshold = [[SettingHandler alloc] initWithTitle:@"Liveness threshold" key:@"liveness_threshold" type:@"slider"];
    SettingHandler *livenessMode = [[SettingHandler alloc] initWithTitle:@"Liveness mode" key:@"liveness_mode" type:@"picker"];
    [livenessMode setOptions:[NSArray arrayWithObjects:@"None",@"Passive", @"Active", @"Passive and active", @"Simple", @"Custom", nil]];
    
    SettingHandler *capturingSettings = [[SettingHandler alloc] initWithTitle:@"Capturing Settings" key:nil type:@"title"];
    SettingHandler *checkIcao = [[SettingHandler alloc] initWithTitle:@"Check ICAO compliance" key:@"check_icao" type:@"switch"];
    SettingHandler *manualCapturing = [[SettingHandler alloc] initWithTitle:@"Manual capturing" key:@"manual_capturing" type:@"switch"];

    SettingHandler *camera = [[SettingHandler alloc] initWithTitle:@"Camera" key:@"camera" type:@"picker"];
    NInt cameraCount;
    NFaceVerificationClientGetAvailableCameraCount(&cameraCount);
    NSMutableArray *cameraList = [NSMutableArray array];
    for (int i = 0; i < cameraCount; i++) {
        NChar* cameraName;
        NInt cameraNameLength;
        NRESULT_CHECK(NFaceVerificationClientGetAvailableCamera(i, &cameraName, &cameraNameLength));
        [cameraList addObject:[NSString stringWithCString:cameraName encoding:NSUTF8StringEncoding]];
    }
    [camera setOptions:cameraList];

    SettingHandler *videoFormat = [[SettingHandler alloc] initWithTitle:@"Video format" key:@"video_format" type:@"picker"];
    HNfvcVideoFormat *formats;
    NInt formatCount;
    NFaceVerificationClientGetAvailableVideoFormats(&formats, &formatCount);
    NSMutableArray *formatList = [NSMutableArray array];
    for (int i = 0; i < formatCount; i++) {
        NUInt width;
        NUInt height;
        NFloat fps;
        NFaceVerificationClientVideoFormatGetWidth(formats[i], &width);
        NFaceVerificationClientVideoFormatGetHeight(formats[i], &height);
        NFaceVerificationClientVideoFormatGetFrameRate(formats[i], &fps);
        [formatList addObject:[NSString stringWithFormat:@"%d x %d @ %d", width, height, (int)fps]];
    }
    
    [videoFormat setOptions:formatList];
    
    SettingHandler *icaoSettings = [[SettingHandler alloc] initWithTitle:@"ICAO Settings" key:nil type:@"title"];
    SettingHandler *saturationThreshold = [[SettingHandler alloc] initWithTitle:@"Saturation threshold" key:@"saturation_threshold" type:@"slider"];
    SettingHandler *sharpnessThreshold = [[SettingHandler alloc] initWithTitle:@"Sharpness threshold" key:@"sharpness_threshold" type:@"slider"];
    SettingHandler *bgUniformityThreshold = [[SettingHandler alloc] initWithTitle:@"BAckground uniformity threshold" key:@"uniformity_threshold" type:@"slider"];
    SettingHandler *grayDensityThreshold = [[SettingHandler alloc] initWithTitle:@"Grayscale density threshold" key:@"gray_threshold" type:@"slider"];
    SettingHandler *lookAwayThreshold = [[SettingHandler alloc] initWithTitle:@"Looking away threshold" key:@"lookaway_threshold" type:@"slider"];
    SettingHandler *redEyeThreshold = [[SettingHandler alloc] initWithTitle:@"Red eye threshold" key:@"redeye_threshold" type:@"slider"];
    SettingHandler *faceDarkThreshold = [[SettingHandler alloc] initWithTitle:@"Face darkness threshold" key:@"facedark_threshold" type:@"slider"];
    SettingHandler *unnaturalThreshold = [[SettingHandler alloc] initWithTitle:@"Unnatural skin tone threshold" key:@"unnatural_threshold" type:@"slider"];
    SettingHandler *washedThreshold = [[SettingHandler alloc] initWithTitle:@"Washed out threshold" key:@"washed_threshold" type:@"slider"];
    SettingHandler *pixelationThreshold = [[SettingHandler alloc] initWithTitle:@"Pixelation threshold" key:@"pixelation_threshold" type:@"slider"];
    SettingHandler *skinReflThreshold = [[SettingHandler alloc] initWithTitle:@"Skin reflection threshold" key:@"skinrefl_threshold" type:@"slider"];
    SettingHandler *glassReflThreshold = [[SettingHandler alloc] initWithTitle:@"Glasses reflection threshold" key:@"glassrefl_threshold" type:@"slider"];
    SettingHandler *expressionThreshold = [[SettingHandler alloc] initWithTitle:@"Expression threshold" key:@"expression_threshold" type:@"slider"];
    SettingHandler *darkGlassesThreshold = [[SettingHandler alloc] initWithTitle:@"Dark glasses threshold" key:@"darkglass_threshold" type:@"slider"];
    SettingHandler *blinkThreshold = [[SettingHandler alloc] initWithTitle:@"Blink threshold" key:@"blink_threshold" type:@"slider"];
    SettingHandler *mouthOpenThreshold = [[SettingHandler alloc] initWithTitle:@"Mouth open threshold" key:@"mouth_threshold" type:@"slider"];
    
    NSArray *settingHandlers = [NSArray arrayWithObjects:verificationSettings, matchingThreshold, extractionSettings, qualityThreshold, livenessSettings, livenessThreshold, livenessMode, capturingSettings, checkIcao, manualCapturing, camera, videoFormat, icaoSettings, saturationThreshold, sharpnessThreshold, bgUniformityThreshold, grayDensityThreshold, lookAwayThreshold, redEyeThreshold, faceDarkThreshold, unnaturalThreshold, washedThreshold, pixelationThreshold, skinReflThreshold, glassReflThreshold, expressionThreshold, darkGlassesThreshold, blinkThreshold, mouthOpenThreshold, nil];
    return settingHandlers;
}
@end
