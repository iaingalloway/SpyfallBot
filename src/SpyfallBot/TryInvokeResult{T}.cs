namespace SpyfallBot
{
    using System.Collections.Generic;

    public struct TryInvokeResult<T>
    {
        public TryInvokeResult(bool success, T value)
            : this()
        {
            this.Success = success;
            this.Value = value;
        }

        public bool Success
        {
            get;
        }

        public T Value
        {
            get;
        }

        public static bool operator ==(TryInvokeResult<T> left, TryInvokeResult<T> right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(TryInvokeResult<T> left, TryInvokeResult<T> right)
        {
            return !left.Equals(right);
        }

        public bool Equals(TryInvokeResult<T> other)
        {
            return this.Success.Equals(other.Success) && EqualityComparer<T>.Default.Equals(this.Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is TryInvokeResult<T> && this.Equals((TryInvokeResult<T>)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (this.Success.GetHashCode() * 397) ^ EqualityComparer<T>.Default.GetHashCode(this.Value);
            }
        }
    }
}
