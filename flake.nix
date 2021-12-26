{
  description = "PuppyPlace Flake";

  inputs = {

    flake-utils = {
      url = "github:numtide/flake-utils";
    };

    nixpkgs = {
      url = "nixpkgs/nixos-21.11";
    };

  };

  outputs = { self, nixpkgs, flake-utils, ... }:
    flake-utils.lib.eachDefaultSystem (system:
      let
        pkgs = nixpkgs.legacyPackages.${system};
      in {
        devShell = pkgs.mkShell {
          buildInputs = with pkgs; [
            postgresql
            powershell
            (with dotnetCorePackages; combinePackages [
              dotnet-sdk_6
              mono
              dotnetPackages.Nuget
            ])
          ];

          shellHook = ''
              export PGHOST=./tmp/postgres
              export PGDATA=$PGHOST/data
              export PGDATABASE=postgres
              export PGLOG=$PGHOST/postgres.log
          '';

          #   mkdir -p $PGHOST

          #   if [ ! -d $PGDATA ]; then
          #   initdb --auth=trust --no-locale --encoding=UTF8
          #   fi

          #   if ! pg_ctl status
          #   then
          #   pg_ctl start -l $PGLOG -o "--unix_socket_directories='$PGHOST' --listen_addresses='''"
          #   fi
          # '';
          # https://jamesmead.org/blog/2020-11-29-multiple-rails-development-environments-using-nix-shell
        };
      });
}
