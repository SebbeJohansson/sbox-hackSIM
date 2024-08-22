

namespace Vrtx.Devices
{
	public class BaseDevice: Component
	{
		[Property]
		required public string Name { get; set; }
		[Property]
		required public string Ip { get; set; }
	}
}
