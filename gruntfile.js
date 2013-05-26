module.exports = function(grunt) {
  grunt.initConfig({
    watch: {
      options: { livereload: true },
      html: {
        files: ['src/FoodCompare.Web/Views/**/*.cshtml']
      }
    }
  });

  grunt.loadNpmTasks('grunt-contrib-watch');
};
