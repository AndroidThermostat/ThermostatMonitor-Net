#!/usr/bin/ruby

require 'open-uri'
require 'httparty'
require 'json'
require 'yaml'
require './thermostat'

class ThermostatMonitor
  attr_reader :thermostats, :weather

  API_URL = 'http://api.thermostatmonitor.com/json/'
  API_KEY = YAML::load_file("api_key.yml")["key"]

  def loadThermostats
    tmData = []
    @thermostats = []
    url = API_URL + '?apiKey=' + API_KEY + '&action=loadSettings'
    open(url) do |f|
      f.each_line {|line| tmData << JSON.parse(line)}
    end
    tmData[0]["thermostats"].each do |t|
      @thermostats << Thermostat.new(t["name"], t["ipAddress"], t["brand"])
    end
  end

  def getJsonData
    tmData = []
    url = API_URL + '?apiKey=' + API_KEY + '&action=loadSettings'
    open(url) do |f|
      f.each_line {|line| tmData << line}
    end
    return tmData
  end

  def loadHashData
    hash = {}
    thermostats = []
    if @thermostats
      @thermostats.each do |t|
        if t.successfulUpdate
          thermostats <<  t.to_hash
        end
      end
      hash["thermostats"] = thermostats
    end
    hash
  end

  def checkin
    tmData = []
    url = API_URL + '?apiKey=' + API_KEY + '&action=checkin'
    @thermostats.each do |t|
      t.update
    end
    res = HTTParty.post(url,
                        :body => loadHashData.to_json,
                        :headers => { 'Content-Type' => 'application/json' } )
    tmData = JSON.parse(res.body)
    if @weather
      @weather.update(tmData["zip"], tmData["outsideTemperature"])
    else
      @weather = Weather.new(tmData["zip"], tmData["outsideTemperature"])
    end

  end

end
