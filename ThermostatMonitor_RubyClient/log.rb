#!/usr/bin/ruby

class LogFile
  attr_accessor :filename, :path, :filepath

  def initialize(filename, path="./")
    @filename = filename
    @path = path
    @filepath = File.join(path, filename)
    logFile = File.open(filepath, "a+")
    logFile.write("Log File initialized, #{Time.now} \n")
    logFile.close
  end

  def puts(data)
    logFile = File.open(self.filepath, "a+")
    logFile.write(data.to_s + "\n")
    logFile.close
  end

end
