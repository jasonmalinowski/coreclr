set(CMAKE_INCLUDE_CURRENT_DIR ON)
set(CMAKE_EXPORT_COMPILE_COMMANDS ON)

include_directories("./jitstd")
include_directories("../inc")

if (CLR_CMAKE_TARGET_ARCH_AMD64 OR CLR_CMAKE_TARGET_ARCH_ARM64 OR (CLR_CMAKE_TARGET_ARCH_I386 AND NOT CLR_CMAKE_PLATFORM_UNIX))
  add_definitions(-DFEATURE_SIMD)
  add_definitions(-DFEATURE_HW_INTRINSICS)
endif ()

# JIT_BUILD disables certain PAL_TRY debugging features
add_definitions(-DJIT_BUILD=1)

if(WIN32)
  set(JIT_RESOURCES Native.rc)
endif(WIN32)

set( JIT_SOURCES
  alloc.cpp
  assertionprop.cpp
  bitset.cpp
  block.cpp
  codegencommon.cpp
  codegenlinear.cpp
  compiler.cpp
  copyprop.cpp
  disasm.cpp
  earlyprop.cpp
  ee_il_dll.cpp
  eeinterface.cpp
  emit.cpp
  error.cpp
  flowgraph.cpp
  gcdecode.cpp
  gcencode.cpp
  gcinfo.cpp
  gentree.cpp
  gschecks.cpp
  hashbv.cpp
  hwintrinsic.cpp
  hostallocator.cpp
  importer.cpp
  inline.cpp
  inlinepolicy.cpp
  instr.cpp
  jitconfig.cpp
  jiteh.cpp
  jittelemetry.cpp
  lclvars.cpp
  lir.cpp
  liveness.cpp
  loopcloning.cpp
  lower.cpp
  lsra.cpp
  morph.cpp
  objectalloc.cpp
  optcse.cpp
  optimizer.cpp
  rangecheck.cpp
  rationalize.cpp
  regalloc.cpp
  register_arg_convention.cpp
  regset.cpp
  scopeinfo.cpp
  sharedfloat.cpp
  sideeffects.cpp
  sm.cpp
  smdata.cpp
  smweights.cpp
  ssabuilder.cpp
  ssarenamestate.cpp
  typeinfo.cpp
  unwind.cpp
  utils.cpp
  valuenum.cpp
)

# Add header files to Visual Studio vcxproj, not required for unixes
# change has effect on editor experience and has no impact on build
if (WIN32)
  set( JIT_HEADERS
    _typeinfo.h
    alloc.h
    arraystack.h
    bitset.h
    bitsetasshortlong.h
    bitsetasuint64.h
    bitsetasuint64inclass.h
    bitsetops.h
    bitvec.h
    block.h
    blockset.h
    codegen.h
    codegenclassic.h
    codegeninterface.h
    codegenlinear.h
    compiler.h
    compiler.hpp
    compilerbitsettraits.h
    compilerbitsettraits.hpp
    compmemkind.h
    compphases.h
    dataflow.h
    decomposelongs.h
    disasm.h
    emit.h
    emitarm.h
    emitarm64.h
    emitdef.h
    emitfmts.h
    emitfmtsarm.h
    emitfmtsarm64.h
    emitfmtsxarch.h
    emitinl.h
    emitjmps.h
    emitpub.h
    emitxarch.h
    error.h
    fp.h
    gentree.h
    gtlist.h
    gtstructs.h
    hashbv.h
    host.h
    hostallocator.h
    hwintrinsiclistxarch.h
    ICorJitInfo_API_names.h
    ICorJitInfo_API_wrapper.hpp
    inline.h
    inlinepolicy.h
    instr.h
    instrs.h
    instrsarm.h
    instrsarm64.h
    instrsxarch.h
    jit.h
    jitconfig.h
    jitconfigvalues.h
    jitee.h
    jiteh.h
    jitexpandarray.h
    jitgcinfo.h
    jithashtable.h
    jitpch.h
    jitstd.h
    jittelemetry.h
    lir.h
    loopcloning.h
    loopcloningopts.h
    lower.h
    lsra_reftypes.h
    lsra.h
    namedintrinsiclist.h
    nodeinfo.h
    objectalloc.h
    opcode.h
    phase.h
    rangecheck.h
    rationalize.h
    regalloc.h
    register_arg_convention.h
    register.h
    registerarm.h
    registerarm64.h
    registerfp.h
    registerxmm.h
    reglist.h
    regpair.h
    regset.h
    sideeffects.h
    simd.h
    simdintrinsiclist.h
    sm.h
    smallhash.h
    smcommon.h
    smopenum.h
    ssabuilder.h
    ssaconfig.h
    ssarenamestate.h
    target.h
    tinyarray.h
    titypes.h
    typelist.h
    unwind.h
    utils.h
    valuenum.h
    valuenumtype.h
    varset.h
    vartype.h
    x86_instrs.h
  )
endif(WIN32)

# The following defines all the source files used by the "legacy" back-end (#ifdef LEGACY_BACKEND).
# It is always safe to include both legacy and non-legacy files in the build, as everything is properly
# #ifdef'ed, though it makes the build slightly slower to do so. Note there is only a legacy backend for
# x86 and ARM.

set(JIT_ARM_LEGACY_SOURCES
    codegenlegacy.cpp
    registerfp.cpp
)
set(JIT_I386_LEGACY_SOURCES
    codegenlegacy.cpp
    stackfp.cpp
)

# Define all the architecture-specific source files

set( JIT_AMD64_SOURCES
  codegenxarch.cpp
  emitxarch.cpp
  lowerxarch.cpp
  lsraxarch.cpp
  simd.cpp
  simdcodegenxarch.cpp
  targetamd64.cpp
  unwindamd64.cpp
  hwintrinsicxarch.cpp
  hwintrinsiccodegenxarch.cpp
)

set( JIT_ARM_SOURCES
  ${JIT_ARM_LEGACY_SOURCES}
  codegenarmarch.cpp
  codegenarm.cpp
  decomposelongs.cpp
  emitarm.cpp
  lowerarmarch.cpp
  lowerarm.cpp
  lsraarmarch.cpp
  lsraarm.cpp
  targetarm.cpp
  unwindarm.cpp
)

set( JIT_I386_SOURCES
  ${JIT_I386_LEGACY_SOURCES}
  codegenxarch.cpp
  decomposelongs.cpp
  emitxarch.cpp
  lowerxarch.cpp
  lsraxarch.cpp
  simd.cpp
  simdcodegenxarch.cpp
  targetx86.cpp
  unwindx86.cpp
  hwintrinsicxarch.cpp
  hwintrinsiccodegenxarch.cpp
)

set( JIT_ARM64_SOURCES
  codegenarmarch.cpp
  codegenarm64.cpp
  emitarm64.cpp
  lowerarmarch.cpp
  lowerarm64.cpp
  lsraarmarch.cpp
  lsraarm64.cpp
  simd.cpp
  targetarm64.cpp
  unwindarm.cpp
  unwindarm64.cpp
  hwintrinsicArm64.cpp
)

if(CLR_CMAKE_TARGET_ARCH_AMD64)
  set(JIT_ARCH_SOURCES ${JIT_AMD64_SOURCES})
elseif(CLR_CMAKE_TARGET_ARCH_ARM)
  set(JIT_ARCH_SOURCES ${JIT_ARM_SOURCES})
elseif(CLR_CMAKE_TARGET_ARCH_I386)
  set(JIT_ARCH_SOURCES ${JIT_I386_SOURCES})
elseif(CLR_CMAKE_TARGET_ARCH_ARM64)
  set(JIT_ARCH_SOURCES ${JIT_ARM64_SOURCES})
else()
  clr_unknown_arch()
endif()

set( SOURCES
  ${JIT_SOURCES}
  ${JIT_HEADERS}
  ${JIT_RESOURCES}
)

convert_to_absolute_path(SOURCES ${SOURCES})
convert_to_absolute_path(JIT_ARCH_SOURCES ${JIT_ARCH_SOURCES})

# Also convert the per-architecture sources to absolute paths, if the subdirs want to use them.

convert_to_absolute_path(JIT_AMD64_SOURCES ${JIT_AMD64_SOURCES})
convert_to_absolute_path(JIT_ARM_SOURCES ${JIT_ARM_SOURCES})
convert_to_absolute_path(JIT_I386_SOURCES ${JIT_I386_SOURCES})
convert_to_absolute_path(JIT_ARM64_SOURCES ${JIT_ARM64_SOURCES})

if(WIN32)
  add_precompiled_header(jitpch.h ../jitpch.cpp SOURCES)

  # Create .def file containing a list of exports preceeded by
  # 'EXPORTS'.  The file "ClrJit.exports" already contains the list, so we
  # massage it into the correct format here to create "ClrJit.exports.def".
  set(JIT_EXPORTS_FILE ${CMAKE_CURRENT_BINARY_DIR}/ClrJit.exports.def)
  set(JIT_EXPORTS_FILE_TEMP ${JIT_EXPORTS_FILE}.txt)
  file(READ "ClrJit.exports" exports_list)
  file(WRITE ${JIT_EXPORTS_FILE_TEMP} "LIBRARY CLRJIT\n")
  file(APPEND ${JIT_EXPORTS_FILE_TEMP} "EXPORTS\n")
  file(APPEND ${JIT_EXPORTS_FILE_TEMP} ${exports_list})

  # Copy the file only if it has changed.
  execute_process(COMMAND ${CMAKE_COMMAND} -E copy_if_different
    ${JIT_EXPORTS_FILE_TEMP} ${JIT_EXPORTS_FILE})

  set(SHARED_LIB_SOURCES ${SOURCES} ${JIT_EXPORTS_FILE})
else()
  set(JIT_EXPORTS_IN_FILE ${CMAKE_CURRENT_BINARY_DIR}/clrjit.exports.in)
  file(READ "${CMAKE_CURRENT_LIST_DIR}/ClrJit.exports" jit_exports)
  file(READ "${CMAKE_CURRENT_LIST_DIR}/ClrJit.PAL.exports" pal_exports)
  file(WRITE ${JIT_EXPORTS_IN_FILE} ${jit_exports})
  file(APPEND ${JIT_EXPORTS_IN_FILE} "\n")
  file(APPEND ${JIT_EXPORTS_IN_FILE} ${pal_exports})

  set(JIT_EXPORTS_FILE ${CMAKE_CURRENT_BINARY_DIR}/clrjit.exports)
  generate_exports_file(${JIT_EXPORTS_IN_FILE} ${JIT_EXPORTS_FILE})

  if(CMAKE_SYSTEM_NAME STREQUAL Linux OR CMAKE_SYSTEM_NAME STREQUAL FreeBSD OR CMAKE_SYSTEM_NAME STREQUAL NetBSD)
    # This is required to force using our own PAL, not one that we are loaded with.
    set(CMAKE_SHARED_LINKER_FLAGS "${CMAKE_SHARED_LINKER_FLAGS} -Xlinker -Bsymbolic -Bsymbolic-functions")

    set(JIT_EXPORTS_LINKER_OPTION -Wl,--version-script=${JIT_EXPORTS_FILE})
  elseif(CMAKE_SYSTEM_NAME STREQUAL Darwin)
    set(JIT_EXPORTS_LINKER_OPTION -Wl,-exported_symbols_list,${JIT_EXPORTS_FILE})
  endif()

  set(SHARED_LIB_SOURCES ${SOURCES})
endif()

add_custom_target(jit_exports DEPENDS ${JIT_EXPORTS_FILE})

set(RYUJIT_LINK_LIBRARIES
   utilcodestaticnohost
   gcinfo
)

if(CLR_CMAKE_PLATFORM_UNIX)
    list(APPEND RYUJIT_LINK_LIBRARIES
       mscorrc_debug
       coreclrpal
       palrt
    )
else()
    list(APPEND RYUJIT_LINK_LIBRARIES
       ${STATIC_MT_CRT_LIB}
       ${STATIC_MT_VCRT_LIB}
       kernel32.lib
       advapi32.lib
       ole32.lib
       oleaut32.lib
       uuid.lib
       user32.lib
       version.lib
       shlwapi.lib
       bcrypt.lib
       crypt32.lib
       RuntimeObject.lib
    )
endif(CLR_CMAKE_PLATFORM_UNIX)

if (FEATURE_MERGE_JIT_AND_ENGINE)
  # Despite the directory being named "dll", it creates a static library "clrjit_static" to link into the VM.
  add_subdirectory(dll)
  add_subdirectory(crossgen)
endif (FEATURE_MERGE_JIT_AND_ENGINE)

add_subdirectory(standalone)

if (CLR_CMAKE_TARGET_ARCH_ARM)
    # Build arm32 legacy_backend to run on both x86 host (crossgen build) and arm host (native).
    add_subdirectory(legacyjit)
endif (CLR_CMAKE_TARGET_ARCH_ARM)

if (CLR_CMAKE_PLATFORM_ARCH_I386 OR CLR_CMAKE_PLATFORM_ARCH_AMD64)
    # On x86, build RyuJIT/ARM32 cross-compiling altjit.
    # On amd64, build RyuJIT/ARM64 cross-compiling altjit.
    add_subdirectory(protononjit)
endif ()

if (CLR_CMAKE_PLATFORM_ARCH_I386)
     # On x86, build RyuJIT/ARM32 cross-compiling altjit for ARM_SOFTFP (armel).
    add_subdirectory(armelnonjit)
endif ()

if ((CLR_CMAKE_PLATFORM_ARCH_I386 OR CLR_CMAKE_PLATFORM_ARCH_AMD64) AND WIN32)
    # On Windows, build altjit that targets the Linux ABI:
    #   On x86, build Linux/x86 altjit. This enables UNIX_X86_ABI.
    #   On amd64, build Linux/AMD64 altjit. This enables UNIX_AMD64_ABI and FEATURE_UNIX_AMD64_STRUCT_PASSING.
    add_subdirectory(linuxnonjit)
endif ()

if (CLR_CMAKE_PLATFORM_ARCH_I386 AND WIN32)
    # On Windows x86, build altjit generating Windows/ARM32 code using LEGACY_BACKEND.
    # (Note: we could also create linuxlegacynonjit for generating Linux/ARM32 code using LEGACY_BACKEND, if needed.)
    add_subdirectory(legacynonjit)
endif (CLR_CMAKE_PLATFORM_ARCH_I386 AND WIN32)
