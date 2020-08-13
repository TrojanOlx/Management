using Snowflake.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Management.Common.Models
{
    public static class GetSnowflakeId
    {
        public static IdWorker Worker = new IdWorker(1,1);
    }
}
