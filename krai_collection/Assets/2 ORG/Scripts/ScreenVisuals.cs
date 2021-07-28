using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using krai_shooter;

namespace krai_shooter
{
    public class ScreenVisuals : MonoBehaviour
    {
        public static ScreenVisuals Singleton;
        //effects
        [SerializeField] Material gunMaterial;
        [SerializeField] Volume vol;
        private Bloom bloom;
        private ChromaticAberration chromaticAbberation;
        private LensDistortion lensDistortion;
        private ColorAdjustments colorAdjustments;
        private void Awake()
        {
            Singleton = this;
        }
        private void Start()
        {
            //post processing effects
            vol.profile.TryGet(out bloom);
            vol.profile.TryGet(out chromaticAbberation);
            vol.profile.TryGet(out lensDistortion);
            vol.profile.TryGet(out colorAdjustments);
        }

        //powerups
        public void ChangeGunTiling()
        {
            Sequence gunTiling = DOTween.Sequence();
            gunTiling.Append(gunMaterial.DOTiling(new Vector2(3f, 3f), 3f));
            gunTiling.Append(gunMaterial.DOTiling(new Vector2(3.6f, 3.6f), 7f));
            gunTiling.Append(gunMaterial.DOTiling(Vector2.one, 0.5f));
        }
        public void Abberation()
        {
            Sequence cameraFov = DOTween.Sequence();
            cameraFov.Append(Camera.main.DOFieldOfView(80, 0.3f));
            cameraFov.Append(Camera.main.DOFieldOfView(80, 3f));
            cameraFov.Append(Camera.main.DOFieldOfView(60, 0.6f));
            Sequence abberation = DOTween.Sequence();
            abberation.Append(DOTween.To(() => chromaticAbberation.intensity.value, x => chromaticAbberation.intensity.value = x, 1, 0.3f));
            abberation.Append(DOTween.To(() => chromaticAbberation.intensity.value, x => chromaticAbberation.intensity.value = x, 1, 8f));
            abberation.Append(DOTween.To(() => chromaticAbberation.intensity.value, x => chromaticAbberation.intensity.value = x, 0, 0.6f));
        }
        public void Bloom()
        {
            Sequence postbloom = DOTween.Sequence();
            postbloom.Append(DOTween.To(() => bloom.intensity.value, x => bloom.intensity.value = x, 2, 0.3f));
            postbloom.Join(DOTween.To(() => bloom.threshold.value, x => bloom.threshold.value = x, 0.6f, 0.3f));
            postbloom.Join(DOTween.To(() => bloom.tint.value, x => bloom.tint.value = x, new Color(231, 65, 197), 0.3f));
            postbloom.Append(DOTween.To(() => bloom.intensity.value, x => bloom.intensity.value = x, 2, 7f));
            postbloom.Join(DOTween.To(() => bloom.threshold.value, x => bloom.threshold.value = x, 0.6f, 7f));
            postbloom.Join(DOTween.To(() => bloom.tint.value, x => bloom.tint.value = x, new Color(231, 65, 197), 7f));
            postbloom.Append(DOTween.To(() => bloom.intensity.value, x => bloom.intensity.value = x, 1, 1f));
            postbloom.Join(DOTween.To(() => bloom.threshold.value, x => bloom.threshold.value = x, 1f, 1f));
            postbloom.Join(DOTween.To(() => bloom.tint.value, x => bloom.tint.value = x, Color.white, 1f));
        }
        public void LensDistortion()
        {
            Sequence lensDist = DOTween.Sequence();
            lensDist.Append(DOTween.To(() => lensDistortion.intensity.value, x => lensDistortion.intensity.value = x, -1, 0.3f));
            lensDist.Append(DOTween.To(() => lensDistortion.intensity.value, x => lensDistortion.intensity.value = x, -1, 5f));
            lensDist.Append(DOTween.To(() => lensDistortion.intensity.value, x => lensDistortion.intensity.value = x, 0, 0.2f));
        }

        public void ColorAdj()
        {
            Sequence coloradj = DOTween.Sequence();
            coloradj.Append(DOTween.To(() => colorAdjustments.postExposure.value, x => colorAdjustments.postExposure.value = x, 0.3f, 0.3f));
            coloradj.Join(DOTween.To(() => colorAdjustments.contrast.value, x => colorAdjustments.contrast.value = x, 100, 0.3f));
            // coloradj.Join(DOTween.To(() => colorAdjustments.colorFilter.value, x => colorAdjustments.colorFilter.value = x, new Color(255, 204, 47), 0.3f));
            coloradj.Append(DOTween.To(() => colorAdjustments.postExposure.value, x => colorAdjustments.postExposure.value = x, 0.3f, 8f));
            coloradj.Join(DOTween.To(() => colorAdjustments.contrast.value, x => colorAdjustments.contrast.value = x, 100f, 8f));
            // coloradj.Join(DOTween.To(() => colorAdjustments.colorFilter.value, x => colorAdjustments.colorFilter.value = x, new Color(255, 204, 47), 3f));
            coloradj.Append(DOTween.To(() => colorAdjustments.postExposure.value, x => colorAdjustments.postExposure.value = x, 0, 0.3f));
            coloradj.Join(DOTween.To(() => colorAdjustments.contrast.value, x => colorAdjustments.contrast.value = x, 0f, 0.3f));
            // coloradj.Join(DOTween.To(() => colorAdjustments.colorFilter.value, x => colorAdjustments.colorFilter.value = x, Color.white, 0.3f));
        }
    }
}
