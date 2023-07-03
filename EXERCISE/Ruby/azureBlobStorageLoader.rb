require 'azure/storage/blob'

def getInputFromUser(message)
  print message
  gets.chomp
end

def printAllBlobsInContainer(blob_client)
  blob_client.list_containers().each do |container|
    puts container.name
  end
end

def createContainerInAzureBlobStorage(blob_client, blob_container_name)
  blob_client.create_container(blob_container_name)
end

def uploadFileToAzureBlob(blob_client, file_path, container_name)
  blob_name = File.basename(file_path)
  blob_client.create_block_blob(container_name, blob_name, File.open(file_path, 'rb'))
end

account_name = ENV['ACCOUNT_NAME']
access_key = ENV['ACCOUNT_KEY']
container_name = get("Enter your container name: ")
file_path = get("Enter your file name (With full path): ")

# Create a blob service client
blob_client = Azure::Storage::Blob::BlobService.create(
  storage_account_name: account_name,
  storage_access_key: access_key
)

createContainerInAzureBlobStorage(blob_client, container_name)


