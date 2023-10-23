using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CcCore.Base.DbManager
{

    [TableName("drives")]
    public class DriveInformationModel:DatabaseModel
    {
        public int DriveId { get; set; }
        public string DriveTag { get; set; }
        public string DriveExtendedTag { get; set; }
        public double DriveCapacity { get; set; }
        public string DriveSerialNumber { get; set; }
        protected override DatabaseContext Context { get; set; }


        public DriveInformationModel(DatabaseContext ctx) => Context = ctx;


        public DriveInformationModel() { }


        public override DatabaseContext GetContext()
        {
            return Context;
        }
    }



    [TableName("connected_history_records")]
    public class ConnecedStatisticsRecords : DatabaseModel
    {
        public int maxUsers { get; set; }
        public string date { get; set; }
        public string game { get; set; }

        protected override DatabaseContext Context { get; set; }


        public ConnecedStatisticsRecords(DatabaseContext ctx) => Context = ctx;


        public ConnecedStatisticsRecords() { }


        public override DatabaseContext GetContext()
        {
            return Context;
        }
    }
}
