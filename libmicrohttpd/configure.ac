AC_INIT([amsrv], [1.0], [oskar@gewalli.se])
AM_INIT_AUTOMAKE([-Wall -Werror foreign])

# Checks for programs.
AC_PROG_CC

# Checks for header files.
AC_HEADER_STDC
AC_CHECK_HEADERS([stdio.h])

# Checks for library functions.
AC_CHECK_FUNCS([getchar])
#
PKG_CHECK_MODULES(MICROHTTPD, libmicrohttpd >= 0.9.42)

AC_CONFIG_HEADERS([src/config.h])
AC_CONFIG_FILES([
 Makefile
 src/Makefile
])

AC_OUTPUT
