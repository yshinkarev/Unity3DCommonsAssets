using Commons.Utils;

namespace Commons.AppEvents
{
    public abstract class AppEvent
    {
        public readonly int Id;

        protected AppEvent()
            : this(0)
        {
        }

        protected AppEvent(int id)
        {
            Id = id;
        }

        public void Fire()
        {
            AppEventList.Get().Fire(this);
        }

        public override string ToString()
        {
            return TypesUtils.GetSimpleClassName(this);
        }
    }
}