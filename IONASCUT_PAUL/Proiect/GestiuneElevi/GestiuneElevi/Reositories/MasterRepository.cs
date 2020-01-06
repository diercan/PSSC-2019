namespace GestiuneElevi.Reositories
{
    public class MasterRepository
    {
        public static IEleviRepository EleviRepository { get; private set; }

        public static void InstantiateEleviRepository()
        {
            if (EleviRepository == null)
                EleviRepository = new EleviRepository();
        }
    }
}
