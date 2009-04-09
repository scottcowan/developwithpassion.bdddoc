require "project_name.rb"

class LocalSettings 
  attr_reader :settings
 def initialize
  @settings = {
  	:app_config_template => 'app.config.xp.template' ,
  	#:app_config_template => 'app.config.vista.template' ,
  	:path_to_runtime_log4net_config => 'artifacts\log4net.config.xml',
  	:initial_catalog => "#{Project.name}",
  	:database_provider => 'System.Data.SqlClient' ,
  	:database_path => 'C:\databases' ,
  	:asp_net_worker_process => 'aspnet_wp.exe',
  	:log_file_name => "#{Project.name}Log.txt",
  	:log_level => 'DEBUG',
  	:xunit_report_file_dir => 'artifacts' ,
  	:xunit_report_file_name => 'test_report',
  	:xunit_report_type => 'text',
  	:xunit_show_test_report => true,
  	:debug => true,
  }
@settings[:xunit_report_file_name_with_extension] = "#{@settings[:xunit_report_file_name]}.#{@settings[:xunit_report_type]}"
@settings[:asp_net_account] = "#{ENV["computername"]}\\ASPNet";
#@settings[:db_account_sql]= "#{@settings[:asp_net_account]} WITH PASSWORD=N'#{@settings[:asp_net_account]}', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF")
@settings[:db_account_sql] = "#{@settings[:asp_net_account]}', N'#{@settings[:asp_net_account]}'"

#these settings may need to be changed to be specific to your local machine
@settings[:osql_connectionstring] = '-E'
@settings[:sql_tools_path] = File.join(ENV['SystemDrive'],'program files/microsoft sql server/100/tools/binn')
@settings[:osql_exe] = File.join("#{@settings[:sql_tools_path]}",'osql.exe')
@settings[:config_connectionstring] = "data source=(local);Integrated Security=SSPI;Initial Catalog=#{@settings[:initial_catalog]}"
 end
end
