using System;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;

namespace Encel.Content
{
    public class DateTimeConverter : IYamlTypeConverter
    {
        public bool Accepts(Type type)
        {
            return type == typeof(DateTime);
        }

        public object ReadYaml(IParser parser, Type type)
        {
            var value = ((Scalar)parser.Current).Value;
            
            parser.MoveNext();

            return DateTime.Parse(value);
        }

        public void WriteYaml(IEmitter emitter, object value, Type type)
        {
            var dateTime = (DateTime)value;
            
            // if datetime has a time specified
            if (dateTime.Ticks != dateTime.Date.Ticks)
            {
                emitter.Emit(new Scalar(dateTime.ToString("g")));  // e.g. 2015-03-08 11:50
            }
            else
            {
                emitter.Emit(new Scalar(dateTime.ToShortDateString())); // e.g. 2015-03-08
            }
        }
    }
}
