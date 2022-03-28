var foo = {};

Object.defineProperty( 
    foo, 
    'bar', 
    { 
      aaa : function() { return 34; }
    } 
  );

foo.aaa();