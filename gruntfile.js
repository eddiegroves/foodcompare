module.exports = function(grunt) {
  grunt.initConfig({
    watch: {
      options: { livereload: true },
      html: {
        files: ['public/index.html']
      },
      css: {
        files: ['public/assets/**/*.css']
      }
    }
  });

  grunt.loadNpmTasks('grunt-contrib-watch');
};
