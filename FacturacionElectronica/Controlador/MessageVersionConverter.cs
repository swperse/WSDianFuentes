using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ServiceModel.Channels;
using System.IO;
using System.ServiceModel.Configuration;
using System.Configuration;
using System.ComponentModel.Design.Serialization;
using System.Text.RegularExpressions;

namespace FacturacionElectronica.Controlador
{
    public class MessageVersionConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (typeof(string) == sourceType)
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (typeof(InstanceDescriptor) == destinationType)
            {
                return true;
            }
            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value is string)
            {
                string messageVersion = (string)value;
                MessageVersion retval = null;
                switch (messageVersion)
                {
                    case ConfigurationStrings.Soap11WSAddressing10:
                        retval = MessageVersion.Soap11WSAddressing10;
                        break;
                    case ConfigurationStrings.Soap12WSAddressing10:
                        retval = MessageVersion.Soap12WSAddressing10;
                        break;
                    case ConfigurationStrings.Soap11WSAddressingAugust2004:
                        retval = MessageVersion.Soap11WSAddressingAugust2004;
                        break;
                    case ConfigurationStrings.Soap12WSAddressingAugust2004:
                        retval = MessageVersion.Soap12WSAddressingAugust2004;
                        break;
                    case ConfigurationStrings.Soap11:
                        retval = MessageVersion.Soap11;
                        break;
                    case ConfigurationStrings.Soap12:
                        retval = MessageVersion.Soap12;
                        break;
                    case ConfigurationStrings.None:
                        retval = MessageVersion.None;
                        break;
                    case ConfigurationStrings.Default:
                        retval = MessageVersion.Default;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("messageVersion");
                }

                return retval;
            }

            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (typeof(string) == destinationType && value is MessageVersion)
            {
                string retval = null;
                MessageVersion messageVersion = (MessageVersion)value;
                if (messageVersion == MessageVersion.Default)
                {
                    retval = ConfigurationStrings.Default;
                }
                else if (messageVersion == MessageVersion.Soap11WSAddressing10)
                {
                    retval = ConfigurationStrings.Soap11WSAddressing10;
                }
                else if (messageVersion == MessageVersion.Soap12WSAddressing10)
                {
                    retval = ConfigurationStrings.Soap12WSAddressing10;
                }
                else if (messageVersion == MessageVersion.Soap11WSAddressingAugust2004)
                {
                    retval = ConfigurationStrings.Soap11WSAddressingAugust2004;
                }
                else if (messageVersion == MessageVersion.Soap12WSAddressingAugust2004)
                {
                    retval = ConfigurationStrings.Soap12WSAddressingAugust2004;
                }
                else if (messageVersion == MessageVersion.Soap11)
                {
                    retval = ConfigurationStrings.Soap11;
                }
                else if (messageVersion == MessageVersion.Soap12)
                {
                    retval = ConfigurationStrings.Soap12;
                }
                else if (messageVersion == MessageVersion.None)
                {
                    retval = ConfigurationStrings.None;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("messageVersion");
                }
                return retval;
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }


    public class ConfigurationStrings
    {
        internal const string MessageVersion = "messageVersion";
        internal const string MediaType = "mediaType";
        internal const string Encoding = "encoding";
        internal const string ReaderQuotas = "readerQuotas";

        internal const string None = "None";
        internal const string Default = "Default";
        internal const string Soap11 = "Soap11";
        internal const string Soap11WSAddressing10 = "Soap11WSAddressing10";
        internal const string Soap11WSAddressingAugust2004 = "Soap11WSAddressingAugust2004";
        internal const string Soap12 = "Soap12";
        internal const string Soap12WSAddressing10 = "Soap12WSAddressing10";
        internal const string Soap12WSAddressingAugust2004 = "Soap12WSAddressingAugust2004";

        internal const string DefaultMessageVersion = Soap11;
        internal const string DefaultMediaType = "text/xml";
        internal const string DefaultEncoding = "utf-8";
    }

    public class MyNewEncodingBindingExtensionElement : BindingElementExtensionElement
    {
        [ConfigurationProperty(ConfigurationStrings.MessageVersion,
            DefaultValue = ConfigurationStrings.DefaultMessageVersion)]
        [TypeConverter(typeof(MessageVersionConverter))]
        public MessageVersion MessageVersion
        {
            get { return (MessageVersion)base[ConfigurationStrings.MessageVersion]; }
            set { base[ConfigurationStrings.MessageVersion] = value; }
        }

        public override void ApplyConfiguration(BindingElement bindingElement)
        {
            base.ApplyConfiguration(bindingElement);
            MyNewEncodingBindingElement binding = (MyNewEncodingBindingElement)bindingElement;
            binding.MessageVersion = this.MessageVersion;
        }
        public override Type BindingElementType
        {
            get { return typeof(MyNewEncodingBindingElement); }
        }

        protected override BindingElement CreateBindingElement()
        {
            var binding = new MyNewEncodingBindingElement();
            ApplyConfiguration(binding);
            return binding;
        }
    }

    public class MyNewEncodingBindingElement : MessageEncodingBindingElement
    {
        private MessageVersion msgVersion;
        private string encoding;

        public MyNewEncodingBindingElement()
        {

        }

        MyNewEncodingBindingElement(MyNewEncodingBindingElement binding)
            : this(binding.Encoding, binding.MessageVersion)
        {

        }

        public MyNewEncodingBindingElement(MessageVersion msgVersion)
        {

            if (msgVersion == null)
                throw new ArgumentNullException("msgVersion");

            this.msgVersion = msgVersion;
        }

        public MyNewEncodingBindingElement(string encoding)
        {
            if (encoding == null)
                throw new ArgumentNullException("encoding");

            this.encoding = encoding;
        }

        public MyNewEncodingBindingElement(string encoding, MessageVersion msgVersion)
        {
            if (encoding == null)
                throw new ArgumentNullException("encoding");

            if (msgVersion == null)
                throw new ArgumentNullException("msgVersion");

            this.msgVersion = msgVersion;
            this.encoding = encoding;
        }

        public override MessageEncoderFactory CreateMessageEncoderFactory()
        {
            return new MyNewEncoderFactory(this.msgVersion);
        }
        public override MessageVersion MessageVersion
        {
            get
            {
                return this.msgVersion;
            }

            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");
                this.msgVersion = value;
            }
        }

        public string Encoding
        {
            get
            {
                return this.encoding;
            }

            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");
                this.encoding = value;
            }
        }
        public override BindingElement Clone()
        {
            return this;
        }
        public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingContext context)
        {
            context.BindingParameters.Add(this);
            return context.BuildInnerChannelFactory<TChannel>();
        }
        public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context)
        {
            context.BindingParameters.Add(this);
            return context.BuildInnerChannelListener<TChannel>();
        }
        public override bool CanBuildChannelFactory<TChannel>(BindingContext context)
        {
            return context.CanBuildInnerChannelFactory<TChannel>();
        }
        public override bool CanBuildChannelListener<TChannel>(BindingContext context)
        {
            return context.CanBuildInnerChannelListener<TChannel>();
        }
    }


    public class MyNewEncoderFactory : MessageEncoderFactory
    {
        MessageVersion messageVersion;
        MyNewEncoder encoder;
        public MyNewEncoderFactory(MessageVersion messageVersion)
        {
            this.messageVersion = messageVersion;
            this.encoder = new MyNewEncoder(messageVersion);
        }
        public override MessageEncoder Encoder
        {
            get { return this.encoder; }
        }
        public override MessageVersion MessageVersion
        {
            get { return this.encoder.MessageVersion; }
        }
    }

    public class MyNewEncoder : MessageEncoder
    {
        MessageEncoder textEncoder;
        MessageEncoder mtomEncoder;
        public MyNewEncoder(MessageVersion messageVersion)
        {
            this.textEncoder = new TextMessageEncodingBindingElement(messageVersion, Encoding.UTF8).CreateMessageEncoderFactory().Encoder;
            this.mtomEncoder = new MtomMessageEncodingBindingElement(messageVersion, Encoding.UTF8).CreateMessageEncoderFactory().Encoder;
        }
        public override string ContentType
        {
            get { return this.textEncoder.ContentType; }
        }
        public override string MediaType
        {
            get { return this.textEncoder.MediaType; }
        }
        public override MessageVersion MessageVersion
        {
            get { return this.textEncoder.MessageVersion; }
        }
        public override bool IsContentTypeSupported(string contentType)
        {
            return this.mtomEncoder.IsContentTypeSupported(contentType);
        }
        public override T GetProperty<T>()
        {
            T result = this.textEncoder.GetProperty<T>();
            if (result == null)
            {
                result = this.mtomEncoder.GetProperty<T>();
            }

            if (result == null)
            {
                result = base.GetProperty<T>();
            }

            return result;
        }
        public override Message ReadMessage(ArraySegment<byte> buffer, BufferManager bufferManager, string contentType)
        {
            //Convert the received buffer into a string
            byte[] incomingResponse = buffer.Array;
            incomingResponse = RemoveSignatures(incomingResponse);

            //read the first 500 bytes of the response
            string strFirst500 = System.Text.Encoding.UTF8.GetString(incomingResponse, 0, 500);

            /*
            * Check the last occurrence of 'application/xop+xml' in the response. We check for the    
            * last occurrence since the first one is present in the Content-Type HTTP Header. Once 
            * found, append charset header to this string
            */

            string modifiedResponse = "";
            int appIndex = strFirst500.LastIndexOf("application/xop+xml");
            if (appIndex > 0  && !strFirst500.Substring(appIndex).Contains("application/xop+xml; charset=utf-8;"))
            {
                modifiedResponse = strFirst500.Insert(appIndex + 19, "; charset=utf-8");
            }

            //convert the modified string back into a byte array
            byte[] ma = System.Text.Encoding.UTF8.GetBytes(modifiedResponse != "" ? modifiedResponse : strFirst500);

            //integrate the modified byte array back to the original byte array
            int increasedLength = ma.Length - 500;
            byte[] newArray = new byte[incomingResponse.Length + increasedLength];

            for (int count = 0; count < newArray.Length; count++)
            {
                if (count < ma.Length)
                {
                    newArray[count] = ma[count];
                }
                else
                {
                    newArray[count] = incomingResponse[count - increasedLength];
                }
            }

            /*
            * In this part generate a new ArraySegment buffer and pass it to the underlying 
            * MTOM Encoder.
            */
            int size = newArray.Length;
            byte[] msg = bufferManager.TakeBuffer(size);
            Array.Copy(newArray, msg, size);
            ArraySegment<byte> newResult = new ArraySegment<byte>(msg);

            Message result = this.mtomEncoder.ReadMessage(newResult, bufferManager, contentType);
            return result;

        }
        public override Message ReadMessage(Stream stream, int maxSizeOfHeaders, string contentType)
        {
            return this.mtomEncoder.ReadMessage(stream, maxSizeOfHeaders, contentType);
        }
        public override ArraySegment<byte> WriteMessage(Message message, int maxMessageSize, BufferManager bufferManager, int messageOffset)
        {
            return this.textEncoder.WriteMessage(message, maxMessageSize, bufferManager, messageOffset);
        }
        public override void WriteMessage(Message message, Stream stream)
        {
            this.textEncoder.WriteMessage(message, stream);
        }

        private byte[] RemoveSignatures(byte[] stream)
        {
            string stream2 = Encoding.UTF8.GetString(stream);
            stream2 = stream2.Replace("\0", "");

            Regex x = new Regex("(\\<SOAP-ENV:Header\\>)(.*?)(\\</SOAP-ENV:Header\\>)");
            string repl = "";
            stream2 = x.Replace(stream2, "$1" + repl + "$3");
            byte[] streamNuevo = Encoding.ASCII.GetBytes(stream2);

            return streamNuevo;
        }
    }
}
