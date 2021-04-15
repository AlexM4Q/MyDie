using Netb.Lib;

namespace Netb
{
    public interface INetbServer
    {
        NetbResponse Handle(NetbRequest request);
    }
}
