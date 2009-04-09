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


deploy_dir = File.join('artifacts','deploy')

#files to deploy
develop_with_passion_bdddoc_logo = File.join('product','images','developwithpassion.bdddoc-logo.jpg')
develop_with_passion_bdddoc_css = File.join('product','config','developwithpassion.bdddoc.css')

output_folders = [project_test_dir]

task :default => [:full_test]

task :init  => :clean do
  mkdir 'artifacts'
  mkdir 'artifacts/coverage'
  mkdir 'artifacts/deploy'
end

desc 'compiles the project'
task :compile => :init do
  MSBuildRunner.compile :compile_target => COMPILE_TARGET, :solution_file => 'solution.sln'
end

desc 'outputs a compiled version of the program to the artifacts/deploy folder'
task :deploy => :compile do
  Dir.glob(File.join('product','**','developwithpassion*.exe')).each do|file|
    FileUtils.cp file,File.join('artifacts','deploy')
  end
  FileUtils.cp develop_with_passion_bdddoc_logo, deploy_dir
  FileUtils.cp develop_with_passion_bdddoc_css, deploy_dir
end

desc 'run the tests for the project'
task :test, :category_to_exclude, :needs => [:compile] do |t,args|
  args.with_defaults(:category_to_exclude => 'SLOW')
  runner = MbUnitRunner.new :compile_target => COMPILE_TARGET, :category_to_exclude => args.category_to_exclude, :show_report => false
  runner.execute_tests ["#{Project.name}.tests"]
end

desc 'run the bdddoc test report for the project'
task :run_test_report => [:test, :deploy] do
 runner = BDDDocRunner.new :bdddoc_folder => File.join('artifacts','deploy')
 runner.run(File.join('product','developwithpassion.bdddoc.tests','bin','debug','developwithpassion.bdddoc.tests.dll'))
end


