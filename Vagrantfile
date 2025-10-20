Vagrant.configure("2") do |config|
  
  config.vm.define "ubuntu" do |ubuntu|
    ubuntu.vm.box = "ubuntu/focal64"
    ubuntu.vm.provision "shell", inline: <<-SHELL
      sudo apt-get update
      sudo apt-get install -y dotnet-sdk-9.0
      mkdir -p /vagrant/app
      cp -r /vagrant/* /vagrant/app
      cd /vagrant/app/VigenereCipherTesting
      dotnet run
    SHELL
  end
end
