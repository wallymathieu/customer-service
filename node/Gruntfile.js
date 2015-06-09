module.exports = function(grunt) {
  // Load Grunt tasks declared in the package.json file.
  require('matchdep').filterDev('grunt-*').forEach(grunt.loadNpmTasks);
  // Project configuration.
  grunt.initConfig({
    /**
     * This will load in our package.json file so we can have access
     * to the project name and appVersion number.
     */
    pkg: grunt.file.readJSON('package.json'),
    /**
     * Constants for the Gruntfile so we can easily change the path for our environments.
     */
    BASE_PATH: '',
    DEVELOPMENT_PATH: '',
    /**
     * Compiles the TypeScript files into one JavaScript file.
     */
    typescript: {
      lib: {
        basePath: '',
        src: ['<%= DEVELOPMENT_PATH %>' + 'lib/CustomerService.ts'],
        options: {
          target: 'es5',
          module: 'AMD',
        }
      },
      jasmine: {
        basePath: '',
        src: ['<%= DEVELOPMENT_PATH %>' + 'spec/runTests.ts'],
        options: {
          target: 'es5',
          module: 'AMD',
        }
      }
    },
    requirejs: {
      compile: {
        options: {
          baseUrl: '<%= DEVELOPMENT_PATH %>' + '', // Path of source scripts, relative to this build file
          mainConfigFile: '<%= DEVELOPMENT_PATH %>' + 'lib/config.js', // Path of shared configuration file, relative to this build file
          name: 'spec/runTests', // Name of input script (.js extension inferred)
          out: '<%= DEVELOPMENT_PATH %>' + 'spec/runSpec.js', // Path of built script output
          fileExclusionRegExp: /.svn/, // Ignore all files matching this pattern
          useStrict: true,
          preserveLicenseComments: false,
          optimize: 'none', // Use 'none' If you do not want to uglify.
        }
      }
    }
  });
  grunt.registerTask('default', ['typescript', 'requirejs']);
};