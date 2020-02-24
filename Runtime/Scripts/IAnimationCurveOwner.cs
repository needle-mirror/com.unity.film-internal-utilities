using UnityEngine;

namespace Unity.FilmInternalUtilities {


public interface IAnimationCurveOwner {
    void SetAnimationCurve(AnimationCurve curve);
    AnimationCurve  GetAnimationCurve();
}

} //end namespace
