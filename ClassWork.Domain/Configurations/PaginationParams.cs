namespace ClassWork.Domain.Configurations
{
    public class PaginationParams
    {
        private const short maxSize = 10;
        private const short minSize = 1;
        private short pageSize = 1;
        private int pageIndex = 1;

        public short PageSize
        {
            get => pageSize;
            set => pageSize = value > maxSize ? maxSize : value < minSize ? minSize : value;
        }

        public int PageIndex
        {
            get => pageIndex;
            set => pageIndex = value < 0 ? pageIndex : value;
        }
    }
}
