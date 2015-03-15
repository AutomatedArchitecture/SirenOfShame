using System;
using System.Threading.Tasks;

namespace SirenOfShame.Extruder.Mac
{
	public class SosOnlineService
	{
		public SosOnlineService ()
		{
		}

		public async Task<ApiResultBase> ConnectExtruder(ConnectExtruderModel connectExtruderModel)
		{
			return await StartRealtimeConnection(connectExtruderModel);
		}

		private async Task<ApiResultBase> StartRealtimeConnection(ConnectExtruderModel connectExtruderModel) {
			return null;
		}
	}
}

