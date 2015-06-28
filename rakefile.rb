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
  msb.prop :platform, "Mixed Platforms"
  msb.target = :Rebuild
  msb.be_quiet
  msb.nologo
  msb.sln =File.join($dir, "CustomerService.sln")
end

task :default => ['build']

