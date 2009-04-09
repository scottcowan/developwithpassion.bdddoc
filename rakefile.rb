require 'build_utilities.rb'
require 'local_properties.rb'
require 'project_name.rb'
require 'rake/clean'
require 'fileutils'

#load settings that differ by machine
local_settings = LocalSettings.new

COMPILE_TARGET = 'debug'

CLEAN.include('artifacts','**/bin','**/obj')

#target folders that can be run from VS
project_test_dir  = File.join('product',"#{Project.name}.tests",'bin','debug')

output_folders = [project_test_dir]

task :default => [:full_test]

task :init  => :clean do
  mkdir 'artifacts'
  mkdir 'artifacts/coverage'
  mkdir 'artifacts/deploy'
end

desc 'compiles the project'
task :compile do
  MSBuildRunner.compile :compile_target => COMPILE_TARGET, :solution_file => 'solution.sln'
end

task :deploy => :compile do
  Dir.glob(File.join('product','**','developwithpassion*.dll')).each do|file|
    FileUtils.cp file,File.join('artifacts','deploy')
  end
end

desc 'run the tests for the project'
task :test, :category_to_exclude, :needs => [:compile] do |t,args|
  args.with_defaults(:category_to_exclude => 'SLOW')
  runner = MbUnitRunner.new :compile_target => COMPILE_TARGET, :category_to_exclude => args.category_to_exclude
  runner.execute_tests ["#{Project.name}.tests"]
end

#	<target name="run" depends="package">
#		<exec program="latestpackage\bdddoc.console.exe"
#			commandline="assets\NothinButDotNetStore.dll TestAttribute SpecReport.html assets\test.report.xml"/>	
#	</target>

