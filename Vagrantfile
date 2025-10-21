Vagrant.configure("2") do |config|
  
  config.vm.define "ubuntu" do |ubuntu|
    ubuntu.vm.box = "ubuntu/focal64"
    ubuntu.vm.provision "shell", inline: <<-SHELL
      sudo apt-get update
      wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
	  sudo dpkg -i packages-microsoft-prod.deb
      sudo apt-get update
      sudo apt-get install -y dotnet-sdk-8.0
      cd /vagrant/src
      dotnet build
    SHELL
  end
end
