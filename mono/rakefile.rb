require 'albacore'
require 'nuget_helper'

$dir = File.dirname(__FILE__)

desc "Install missing NuGet packages."
task :install_packages do
  NugetHelper.exec("restore CustomerService.sln")
end

desc "build"
build :build => [:install_packages] do |msb|
  msb.prop :configuration, :Debug
  msb.prop :platform, "Any CPU"
  msb.target = :Rebuild
  msb.be_quiet
  msb.nologo
  msb.sln =File.join($dir, "CustomerService.sln")
end

task :default => ['build']

desc "test using nunit console"
test_runner :test => [:build] do |nunit|
  nunit.exe = NugetHelper.nunit_path
  files = Dir.glob(File.join($dir,"*Tests","bin","**","*Tests.dll")) 
  nunit.files = files 
end
