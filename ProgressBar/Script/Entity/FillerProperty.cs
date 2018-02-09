namespace ProgressBar.Entity
{
    public class FillerProperty
    {
        public float MaxWidth;
        public float MinWidth;

        public FillerProperty(float min, float max)
        {
            MinWidth = min;
            MaxWidth = max;
        }
    }
}