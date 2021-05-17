namespace CSBUnlimited.Utils.Dsv.Core
{
    public interface IDsvWriter<T> where T : class
    {
        void WriteData(T record);
    }
}
