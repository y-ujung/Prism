using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace JsonXml.Content.Class
{
    public class XmlJsonWriter : JsonWriter
    {
        private readonly XmlWriter _writer;
        private string _propertyName;

        public XmlJsonWriter(XmlWriter writer)
        {
            _writer = writer;
        }

        public override void WriteComment(string text)
        {
            base.WriteComment(text);
            _writer.WriteComment(text);
        }

        public override void WritePropertyName(string name)
        {
            base.WritePropertyName(name);
            _propertyName = name;
        }

        public override void WriteNull()
        {
            base.WriteNull();

            WriteValueElement(JTokenType.Null);
            _writer.WriteEndElement();
        }

        public override void WriteValue(DateTime value)
        {
            base.WriteValue(value);

            WriteValueElement(JTokenType.Date);
            _writer.WriteValue(value);
            _writer.WriteEndElement();
        }

        public override void WriteValue(DateTimeOffset value)
        {
            base.WriteValue(value);

            WriteValueElement(JTokenType.Date);
            _writer.WriteValue(value);
            _writer.WriteEndElement();
        }

        public override void WriteValue(Guid value)
        {
            base.WriteValue(value);

            WriteValueElement(JTokenType.Guid);
            _writer.WriteValue(value.ToString());
            _writer.WriteEndElement();
        }

        public override void WriteValue(TimeSpan value)
        {
            base.WriteValue(value);

            WriteValueElement(JTokenType.TimeSpan);
            _writer.WriteValue(value);
            _writer.WriteEndElement();
        }

        public override void WriteValue(Uri value)
        {
            base.WriteValue(value);

            WriteValueElement(JTokenType.Uri);
            _writer.WriteValue(value);
            _writer.WriteEndElement();
        }

        public override void WriteValue(string value)
        {
            base.WriteValue(value);

            WriteValueElement(JTokenType.String);
            _writer.WriteValue(value);
            _writer.WriteEndElement();
        }

        public override void WriteValue(int value)
        {
            base.WriteValue(value);

            WriteValueElement(JTokenType.Integer);
            _writer.WriteValue(value);
            _writer.WriteEndElement();
        }

        public override void WriteValue(long value)
        {
            base.WriteValue(value);

            WriteValueElement(JTokenType.Integer);
            _writer.WriteValue(value);
            _writer.WriteEndElement();
        }

        public override void WriteValue(short value)
        {
            base.WriteValue(value);

            WriteValueElement(JTokenType.Integer);
            _writer.WriteValue(value);
            _writer.WriteEndElement();
        }

        public override void WriteValue(byte value)
        {
            base.WriteValue(value);

            WriteValueElement(JTokenType.Integer);
            _writer.WriteValue(value);
            _writer.WriteEndElement();
        }

        public override void WriteValue(bool value)
        {
            base.WriteValue(value);

            WriteValueElement(JTokenType.Boolean);
            _writer.WriteValue(value);
            _writer.WriteEndElement();
        }

        public override void WriteValue(char value)
        {
            base.WriteValue(value);

            WriteValueElement(JTokenType.String);
            _writer.WriteValue(value.ToString(CultureInfo.InvariantCulture));
            _writer.WriteEndElement();
        }

        public override void WriteValue(decimal value)
        {
            base.WriteValue(value);

            WriteValueElement(JTokenType.Float);
            _writer.WriteValue(value);
            _writer.WriteEndElement();
        }

        public override void WriteValue(double value)
        {
            base.WriteValue(value);

            WriteValueElement(JTokenType.Float);
            _writer.WriteValue(value);
            _writer.WriteEndElement();
        }

        public override void WriteValue(float value)
        {
            base.WriteValue(value);

            WriteValueElement(JTokenType.Float);
            _writer.WriteValue(value);
            _writer.WriteEndElement();
        }

        private void WriteValueElement(JTokenType type)
        {
            if (_propertyName != null)
            {
                WriteValueElement(_propertyName, type);
                _propertyName = null;
            }
            else
            {
                WriteValueElement("Item", type);
            }
        }

        private void WriteValueElement(string elementName, JTokenType type)
        {
            _writer.WriteStartElement(elementName);
            _writer.WriteAttributeString("type", type.ToString());
        }

        public override void WriteStartArray()
        {
            bool isStart = (WriteState == Newtonsoft.Json.WriteState.Start);

            base.WriteStartArray();

            if (isStart)
            {
                WriteValueElement("Root", JTokenType.Array);
            }
            else
            {
                WriteValueElement(JTokenType.Array);
            }
        }

        public override void WriteStartObject()
        {
            bool isStart = (WriteState == Newtonsoft.Json.WriteState.Start);

            base.WriteStartObject();

            if (isStart)
            {
                WriteValueElement("Root", JTokenType.Object);
            }
            else
            {
                WriteValueElement(JTokenType.Object);
            }
        }

        public override void WriteStartConstructor(string name)
        {
            bool isStart = (WriteState == Newtonsoft.Json.WriteState.Start);

            base.WriteStartConstructor(name);

            if (isStart)
            {
                WriteValueElement("Root", JTokenType.Constructor);
            }
            else
            {
                WriteValueElement(JTokenType.Constructor);
            }

            _writer.WriteAttributeString("name", name);
        }

        public override void WriteEndArray()
        {
            base.WriteEndArray();
            _writer.WriteEndElement();
        }

        public override void WriteEndObject()
        {
            base.WriteEndObject();
            _writer.WriteEndElement();
        }

        public override void WriteEndConstructor()
        {
            base.WriteEndConstructor();
            _writer.WriteEndElement();
        }

        public override void Flush()
        {
            _writer.Flush();
        }

        protected override void WriteIndent()
        {
            _writer.WriteWhitespace(Environment.NewLine);

            // levels of indentation multiplied by the indent count
            int currentIndentCount = Top * 2;

            while (currentIndentCount > 0)
            {
                // write up to a max of 10 characters at once to avoid creating too many new strings
                int writeCount = Math.Min(currentIndentCount, 10);

                _writer.WriteWhitespace(new string(' ', writeCount));

                currentIndentCount -= writeCount;
            }
        }
    }
}
