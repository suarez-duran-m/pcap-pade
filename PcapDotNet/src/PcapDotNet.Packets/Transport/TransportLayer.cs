using PcapDotNet.Packets.IpV4;

namespace PcapDotNet.Packets.Transport
{
    /// <summary>
    /// Contains the common part of UDP and TCP layers.
    /// <seealso cref="TransportDatagram"/>
    /// </summary>
    public abstract class TransportLayer : Layer, IIpV4NextTransportLayer
    {
        /// <summary>
        /// Checksum is the 16-bit one's complement of the one's complement sum of a pseudo header of information from the IP header, 
        /// the Transport header, and the data, padded with zero octets at the end (if necessary) to make a multiple of two octets.
        /// </summary>
        public ushort? Checksum { get; set; }

        /// <summary>
        /// Indicates the port of the sending process.
        /// In UDP, this field is optional and may only be assumed to be the port 
        /// to which a reply should be addressed in the absence of any other information.
        /// If not used in UDP, a value of zero is inserted.
        /// </summary>
        public ushort SourcePort { get; set; }

        /// <summary>
        /// Destination Port has a meaning within the context of a particular internet destination address.
        /// </summary>
        public ushort DestinationPort { get; set; }

        /// <summary>
        /// The protocol that should be written in the previous (IPv4) layer.
        /// </summary>
        public abstract IpV4Protocol PreviousLayerProtocol { get; }

        /// <summary>
        /// Whether the checksum should be calculated.
        /// Can be false in UDP because the checksum is optional. false means that the checksum will be left zero.
        /// </summary>
        public virtual bool CalculateChecksum
        {
            get { return true; }
        }

        /// <summary>
        /// The offset in the layer where the checksum should be written.
        /// </summary>
        public abstract int ChecksumOffset { get; }

        /// <summary>
        /// Whether the checksum is optional in the layer.
        /// </summary>
        public abstract bool IsChecksumOptional { get; }

        public virtual bool Equals(TransportLayer other)
        {
            return other != null &&
                   PreviousLayerProtocol == other.PreviousLayerProtocol &&
                   Checksum == other.Checksum &&
                   SourcePort == other.SourcePort && DestinationPort == other.DestinationPort;
        }

        public override sealed bool Equals(Layer other)
        {
            return base.Equals(other) && Equals(other as TransportLayer);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode() ^
                   Checksum.GetHashCode() ^
                   ((SourcePort << 16) + DestinationPort);
        }
    }
}