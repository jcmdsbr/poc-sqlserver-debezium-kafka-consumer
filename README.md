# CDC POC :sunglasses:

- POC from change data capture context use debezium and kafka

## Give a Star! :star:

If you liked the project, please give a star ;)

## Setup :books:

### Start the topology as defined in [Tutorial](https://debezium.io/documentation/reference/stable/tutorial.html)

docker-compose -f docker-compose.yml up

### Initialize database and insert test data

cat ../scripts/inventory.sql | docker-compose -f docker-compose.yml exec -T sqlserver bash -c '/opt/mssql-tools/bin/sqlcmd -U sa -P Password!'

### Start SQL Server connector

curl -i -X POST -H "Accept:application/json" -H  "Content-Type:application/json" http://localhost:8083/connectors/ -d @../scripts/register-sqlserver.json
