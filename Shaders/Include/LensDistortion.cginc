// Brown-Conrady distortion model.
//lensDistortionMainParams. x: k1, y: k2, z: cx, w: cy
//Returns distorted position in clip space (positionCS)
float2 CalculateDistortedPosition(const float2 ndc, const float overscanScale,
    const float4 lensDistortionMainParams, const float2 sensorSize)
{
    //Translate parameters
    const float k1 = lensDistortionMainParams.x;
    const float k2 = lensDistortionMainParams.y;
    const float cx = lensDistortionMainParams.z;
    const float cy = lensDistortionMainParams.w;

    const float paW = sensorSize.x;
    const float paH = sensorSize.y;
    
    float2 posD = float2(
        (ndc.x - (_ScreenSize.x - 1) / 2) / (_ScreenSize.x - 1) * paW,
        (ndc.y - (_ScreenSize.y - 1) / 2) / (_ScreenSize.y - 1) * paH
        );
    const float r2 = pow(posD.x + cx, 2) + pow(posD.y + cy, 2);
    const float f = 1 + r2 * k1 + r2 * r2 * k2;

    float2 posP = float2(f * (posD.x + cx), f * (posD.y + cy));
    float2 distorted = float2(
        round((_ScreenSize.x * 1.0 / 2 - 0.5f) + posP.x * ((_ScreenSize.x - 1) / paW)),
        round((_ScreenSize.y * 1.0 / 2 - 0.5f) + posP.y * ((_ScreenSize.y - 1) / paH))
        );

    float2 result = float2(
        _ScreenSize.x * 0.5f + (distorted.x - _ScreenSize.x * 0.5f) / overscanScale,
        _ScreenSize.y * 0.5f + (distorted.y - _ScreenSize.y * 0.5f) / overscanScale);

    return result;
}
