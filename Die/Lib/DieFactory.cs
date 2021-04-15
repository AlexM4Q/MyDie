namespace MyDie.Die.Lib
{
    public delegate T DieFactory<out T>(IDieContainer container) where T : class;
}
