//HACK: this file will overload the bootstrap.js requirement from WFFM and make sure that bootstrap.js is not loaded twice.
if (typeof require !== "undefined") {
  require.config(
  {
    paths: {
      bootstrap: "/scripts/DisableWFFMBootstrap"
    }
  });
}
