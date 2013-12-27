using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DR.Async.Task.Helpers
{
    public static class Monads
    {
        public static TResult With<TInput, TResult>(this TInput o, Func<TInput, TResult> evaluator)
            where TResult : class
            where TInput : class
        {
            return o == null ? null : evaluator(o);
        }

        public static TResult Return<TInput, TResult>(
            this TInput o, Func<TInput, TResult> evaluator, TResult failureValue)
            where TInput : class
        {
            return o == null ? failureValue : evaluator(o);
        }

        public static TInput If<TInput>(this TInput o, Predicate<TInput> evaluator)
            where TInput : class
        {
            return o == null ? null : (evaluator(o) ? o : null);
        }

        public static TInput Unless<TInput>(this TInput o, Predicate<TInput> evaluator)
            where TInput : class
        {
            return o == null ? null : (evaluator(o) ? null : o);
        }

        public static TInput Do<TInput>(this TInput o, Action<TInput> action) where TInput : class
        {
            if (o == null)
            {
                return null;
            }

            action(o);
            return o;
        }

        public static T NotNull<T>(this T value) where T : class
        {
            if (value == null)
            {
                throw new ArgumentNullException(
                    "Value of type '{0}' is null".FormatString(typeof(T).Name));
            }

            return value;
        }

        public static T NotNull<T>(this T? value) where T : struct
        {
            if (value == null)
            {
                throw new ArgumentNullException(
                    "Value of type '{0}' is null".FormatString(typeof(T).Name));
            }

            return value.Value;
        }

        public static TInputBase DoTypeSwitch<TInputBase, TInput1>(
            this TInputBase source,
            Action<TInput1> func1)
            where TInputBase : class
        {
            func1.NotNull();

            if (source is TInput1)
            {
                func1((TInput1)(object)source);
                return source;
            }

            return source;
        }

        public static TInputBase DoTypeSwitch<TInputBase, TInput1>(
            this TInputBase source,
            Action<TInput1> func1,
            Action<TInputBase> funcBase)
            where TInputBase : class
        {
            func1.NotNull();
            funcBase.NotNull();

            if (source is TInput1)
            {
                func1((TInput1)(object)source);
                return source;
            }

            funcBase(source);
            return source;
        }

        public static TInputBase DoTypeSwitch<TInputBase, TInput1, TInput2>(
            this TInputBase source,
            Action<TInput1> func1,
            Action<TInput2> func2,
            Action<TInputBase> funcBase)
            where TInputBase : class
        {
            func1.NotNull();
            func2.NotNull();
            funcBase.NotNull();

            if (source is TInput1)
            {
                func1((TInput1)(object)source);
                return source;
            }

            if (source is TInput2)
            {
                func2((TInput2)(object)source);
                return source;
            }

            funcBase(source);
            return source;
        }

        public static TInputBase DoTypeSwitch<TInputBase, TInput1, TInput2, TInput3>(
            this TInputBase source,
            Action<TInput1> func1,
            Action<TInput2> func2,
            Action<TInput3> func3,
            Action<TInputBase> funcBase)
            where TInputBase : class
        {
            func1.NotNull();
            func2.NotNull();
            func3.NotNull();
            funcBase.NotNull();

            if (source is TInput1)
            {
                func1((TInput1)(object)source);
                return source;
            }

            if (source is TInput2)
            {
                func2((TInput2)(object)source);
                return source;
            }

            if (source is TInput3)
            {
                func3((TInput3)(object)source);
                return source;
            }

            funcBase(source);
            return source;
        }

        public static TInputBase DoTypeSwitch<TInputBase, TInput1, TInput2, TInput3, TInput4>(
            this TInputBase source,
            Action<TInput1> func1,
            Action<TInput2> func2,
            Action<TInput3> func3,
            Action<TInput4> func4,
            Action<TInputBase> funcBase)
            where TInputBase : class
        {
            func1.NotNull();
            func2.NotNull();
            func3.NotNull();
            func4.NotNull();
            funcBase.NotNull();

            if (source is TInput1)
            {
                func1((TInput1)(object)source);
                return source;
            }

            if (source is TInput2)
            {
                func2((TInput2)(object)source);
                return source;
            }

            if (source is TInput3)
            {
                func3((TInput3)(object)source);
                return source;
            }

            if (source is TInput4)
            {
                func4((TInput4)(object)source);
                return source;
            }

            funcBase(source);
            return source;
        }

        public static TInputBase DoTypeSwitch<TInputBase, TInput1, TInput2, TInput3, TInput4, TInput5>(
            this TInputBase source,
            Action<TInput1> func1,
            Action<TInput2> func2,
            Action<TInput3> func3,
            Action<TInput4> func4,
            Action<TInput5> func5,
            Action<TInputBase> funcBase)
            where TInputBase : class
        {
            func1.NotNull();
            func2.NotNull();
            func3.NotNull();
            func4.NotNull();
            func5.NotNull();
            funcBase.NotNull();

            if (source is TInput1)
            {
                func1((TInput1)(object)source);
                return source;
            }

            if (source is TInput2)
            {
                func2((TInput2)(object)source);
                return source;
            }

            if (source is TInput3)
            {
                func3((TInput3)(object)source);
                return source;
            }

            if (source is TInput4)
            {
                func4((TInput4)(object)source);
                return source;
            }

            if (source is TInput5)
            {
                func5((TInput5)(object)source);
                return source;
            }

            funcBase(source);
            return source;
        }

        public static TResult TypeSwitch<TInputBase, TInput1, TResult>(
            this TInputBase source,
            Func<TInput1, TResult> func1,
            TResult failureValue)
            where TInputBase : class
        {
            func1.NotNull();

            if (source is TInput1)
            {
                return func1((TInput1)(object)source);
            }

            return failureValue;
        }

        public static TResult TypeSwitch<TInputBase, TInput1, TResult>(
            this TInputBase source,
            Func<TInput1, TResult> func1,
            Func<TInputBase, TResult> funcBase)
            where TInputBase : class
        {
            func1.NotNull();
            funcBase.NotNull();

            if (source is TInput1)
            {
                return func1((TInput1)(object)source);
            }

            return funcBase(source);
        }

        public static TResult TypeSwitch<TInputBase, TInput1, TInput2, TResult>(
            this TInputBase source,
            Func<TInput1, TResult> func1,
            Func<TInput2, TResult> func2,
            Func<TInputBase, TResult> funcBase)
            where TInputBase : class
        {
            func1.NotNull();
            func2.NotNull();
            funcBase.NotNull();

            if (source is TInput1)
            {
                return func1((TInput1)(object)source);
            }

            if (source is TInput2)
            {
                return func2((TInput2)(object)source);
            }

            return funcBase(source);
        }

        public static TResult TypeSwitch<TInputBase, TInput1, TInput2, TInput3, TResult>(
            this TInputBase source,
            Func<TInput1, TResult> func1,
            Func<TInput2, TResult> func2,
            Func<TInput3, TResult> func3,
            Func<TInputBase, TResult> funcBase)
            where TInputBase : class
        {
            func1.NotNull();
            func2.NotNull();
            func3.NotNull();
            funcBase.NotNull();

            if (source is TInput1)
            {
                return func1((TInput1)(object)source);
            }

            if (source is TInput2)
            {
                return func2((TInput2)(object)source);
            }

            if (source is TInput3)
            {
                return func3((TInput3)(object)source);
            }

            return funcBase(source);
        }

        public static TResult TypeSwitch<TInputBase, TInput1, TInput2, TInput3, TInput4, TResult>(
            this TInputBase source,
            Func<TInput1, TResult> func1,
            Func<TInput2, TResult> func2,
            Func<TInput3, TResult> func3,
            Func<TInput4, TResult> func4,
            Func<TInputBase, TResult> funcBase)
            where TInputBase : class
        {
            func1.NotNull();
            func2.NotNull();
            func3.NotNull();
            func4.NotNull();
            funcBase.NotNull();

            if (source is TInput1)
            {
                return func1((TInput1)(object)source);
            }

            if (source is TInput2)
            {
                return func2((TInput2)(object)source);
            }

            if (source is TInput3)
            {
                return func3((TInput3)(object)source);
            }

            if (source is TInput4)
            {
                return func4((TInput4)(object)source);
            }

            return funcBase(source);
        }

        public static TResult TypeSwitch<TInputBase, TInput1, TInput2, TInput3, TInput4, TInput5, TResult>(
            this TInputBase source,
            Func<TInput1, TResult> func1,
            Func<TInput2, TResult> func2,
            Func<TInput3, TResult> func3,
            Func<TInput4, TResult> func4,
            Func<TInput5, TResult> func5,
            Func<TInputBase, TResult> funcBase)
            where TInputBase : class
        {
            func1.NotNull();
            func2.NotNull();
            func3.NotNull();
            func4.NotNull();
            func5.NotNull();
            funcBase.NotNull();

            if (source is TInput1)
            {
                return func1((TInput1)(object)source);
            }

            if (source is TInput2)
            {
                return func2((TInput2)(object)source);
            }

            if (source is TInput3)
            {
                return func3((TInput3)(object)source);
            }

            if (source is TInput4)
            {
                return func4((TInput4)(object)source);
            }

            if (source is TInput5)
            {
                return func5((TInput5)(object)source);
            }

            return funcBase(source);
        }
    }
}
