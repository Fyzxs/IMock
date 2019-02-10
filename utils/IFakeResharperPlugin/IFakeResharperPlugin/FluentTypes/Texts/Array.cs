namespace Fyzxs.IFakeResharperPlugin.FluentTypes.Texts {
    public abstract class Array<T>
    {
        public static implicit operator string[] (Array<T> origin) => origin.RawValue();

        protected abstract string[] RawValue();

    }
}