namespace Commons.Helper
{
    public class ParticleDurationStore
    {
        #region Static
        private static ParticleDurationStore _instance;

        public static ParticleDurationStore Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ParticleDurationStore();

                return _instance;
            }
        }
        #endregion

//        private readonly Dictionary<> 
//
//        public float GetDuration(GameObject go)
//        {
//            
//        }
    }
}
