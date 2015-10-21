namespace SpyfallBot
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    public static class TryInvoke<TException>
        where TException : Exception
    {
        [SuppressMessage("Microsoft.Design", "CA1000:DoNotDeclareStaticMembersOnGenericTypes")]
        [SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
        public static TryInvokeResult<T> Func<T>(Func<T> body)
        {
            if (body == null)
            {
                throw new ArgumentNullException(nameof(body));
            }

            try
            {
                var value = body();
                return new TryInvokeResult<T>(true, value);
            }
            catch (TException)
            {
                return default(TryInvokeResult<T>);
            }
        }
    }
}
