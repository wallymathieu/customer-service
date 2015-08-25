ENV['RACK_ENV'] = 'test'
require_relative '../app/app'
require 'minitest/autorun'
require 'rack/test'