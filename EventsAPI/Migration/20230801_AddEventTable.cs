using FluentMigrator;

namespace EventsAPI.Migration {
    [Migration(202308011353)]
    public class AddEventTable : FluentMigrator.Migration {
        public override void Down() {
            Delete.Table("Event");
        }

        public override void Up() {
            Create.Table("Event")
                .WithColumn("Id").AsString().Unique().PrimaryKey()
                .WithColumn("Name").AsString()
                .WithColumn("Description").AsString()
                .WithColumn("EventType").AsInt32()
                .WithColumn("Date").AsDate();
        }
    }
}
