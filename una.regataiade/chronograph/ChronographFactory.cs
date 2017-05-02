namespace una.regataiade.chronograph
{
    public static class ChronographFactory
    {
        public static AbstractChronograph CreateCP520Chronograph()
        {
            return new ChronographCP520();
        }

        public static AbstractChronograph CreateCP502Chronograph()
        {
            return new ChronographCP502();
        }
    }
}
